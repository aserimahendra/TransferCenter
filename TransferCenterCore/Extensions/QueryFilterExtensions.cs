using System.Linq.Expressions;

namespace TransferCenterCore.Extensions;

public static class QueryFilterExtensions
{
    public static FilterBuilder<T> StartBuilder<T>(this IQueryable<T> source) where T : class
        => new FilterBuilder<T>(source);

    public sealed class FilterBuilder<T> where T : class
    {
        private IQueryable<T> _query;

        internal FilterBuilder(IQueryable<T> source)
        {
            _query = source;
        }

        public FilterBuilder<T> ByEquals(string propertyName, object? value)
        {
            if (value == null) return this;
            var parameter = Expression.Parameter(typeof(T), "x");
            var member = BuildMemberAccess(parameter, propertyName);

            // Convert value to the member type if needed
            var targetType = Nullable.GetUnderlyingType(member.Type) ?? member.Type;
            var convertedValue = Convert.ChangeType(value, targetType);
            var constant = Expression.Constant(convertedValue, targetType);

            Expression left = member;
            if (member.Type != targetType)
            {
                // Handle nullable to non-nullable comparisons
                if (Nullable.GetUnderlyingType(member.Type) != null)
                {
                    left = Expression.Property(member, "Value");
                }
                else
                {
                    left = Expression.Convert(member, targetType);
                }
            }

            var body = Expression.Equal(left, constant);
            var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);
            _query = _query.Where(lambda);
            return this;
        }

        public FilterBuilder<T> ByContains(string propertyName, string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return this;
            var term = value.Trim().ToLowerInvariant();

            var parameter = Expression.Parameter(typeof(T), "x");
            var member = BuildMemberAccess(parameter, propertyName);
            if (member.Type != typeof(string)) return this; // only apply to string properties

            var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes)!;
            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;

            var notNull = Expression.NotEqual(member, Expression.Constant(null, typeof(string)));
            var lower = Expression.Call(member, toLowerMethod);
            var constant = Expression.Constant(term);
            var contains = Expression.Call(lower, containsMethod, constant);
            var body = Expression.AndAlso(notNull, contains);

            var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);
            _query = _query.Where(lambda);
            return this;
        }

        /// <summary>
        /// Performs a case-insensitive Contains against ANY of the provided string properties (logical OR).
        /// Each property is independently checked for null before calling Contains. Non-string properties are skipped.
        /// Usage: source.StartBuilder().ByAnyContains(new[]{"FirstName","LastName"}, search).Build();
        /// </summary>
        public FilterBuilder<T> ByAnyContains(IEnumerable<string> propertyNames, string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return this;
            if (propertyNames == null) return this;
            var names = propertyNames.Where(n => !string.IsNullOrWhiteSpace(n)).ToArray();
            if (names.Length == 0) return this;

            var term = value.Trim().ToLowerInvariant();
            var parameter = Expression.Parameter(typeof(T), "x");
            var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes)!;
            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
            var constant = Expression.Constant(term);

            Expression? orExpression = null;
            foreach (var name in names)
            {
                var member = BuildMemberAccess(parameter, name);
                if (member.Type != typeof(string)) continue;

                var notNull = Expression.NotEqual(member, Expression.Constant(null, typeof(string)));
                var lower = Expression.Call(member, toLowerMethod);
                var contains = Expression.Call(lower, containsMethod, constant);
                var andExpr = Expression.AndAlso(notNull, contains);
                orExpression = orExpression == null ? andExpr : Expression.OrElse(orExpression, andExpr);
            }

            if (orExpression == null) return this; // no valid string members

            var lambda = Expression.Lambda<Func<T, bool>>(orExpression, parameter);
            _query = _query.Where(lambda);
            return this;
        }

        /// <summary>
        /// Generic apply method: if value is null (or empty string) it is skipped.
        /// Strings use case-insensitive Contains by default (override via useContainsForStrings=false for Equals).
        /// Other primitive / value types use equality comparison.
        /// </summary>
        /// <param name="propertyName">Name (optionally dotted path) of the property on T.</param>
        /// <param name="value">Value to filter by.</param>
        /// <param name="useContainsForStrings">If true and value is string, performs Contains (case-insensitive); otherwise Equals.</param>
        public FilterBuilder<T> Apply(string propertyName, object? value, bool useContainsForStrings = true)
        {
            if (value == null) return this;
            if (value is string s)
            {
                if (string.IsNullOrWhiteSpace(s)) return this;
                return useContainsForStrings ? ByContains(propertyName, s) : ByEquals(propertyName, s);
            }
            return ByEquals(propertyName, value);
        }

        public FilterBuilder<T> ByDateFrom(string propertyName, DateTime? from)
        {
            if (!from.HasValue) return this;
            var parameter = Expression.Parameter(typeof(T), "x");
            var member = BuildMemberAccess(parameter, propertyName);

            var dateExpr = AsDateTime(member);
            if (dateExpr == null) return this;

            var fromDate = Expression.Constant(from.Value.Date, typeof(DateTime));
            var body = Expression.GreaterThanOrEqual(dateExpr, fromDate);
            var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);
            _query = _query.Where(lambda);
            return this;
        }

        public FilterBuilder<T> ByDateTo(string propertyName, DateTime? to)
        {
            if (!to.HasValue) return this;
            var parameter = Expression.Parameter(typeof(T), "x");
            var member = BuildMemberAccess(parameter, propertyName);

            var dateExpr = AsDateTime(member);
            if (dateExpr == null) return this;

            // Compare strictly less than next day to include the entire 'to' day regardless of time.
            var nextDay = to.Value.Date.AddDays(1);
            var nextDayConst = Expression.Constant(nextDay, typeof(DateTime));
            var body = Expression.LessThan(dateExpr, nextDayConst);
            var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);
            _query = _query.Where(lambda);
            return this;
        }

        public IQueryable<T> Build() => _query;

        private static MemberExpression BuildMemberAccess(Expression parameter, string propertyPath)
        {
            var parts = propertyPath.Split('.');
            Expression current = parameter;
            foreach (var part in parts)
            {
                current = Expression.PropertyOrField(current, part);
            }
            return (MemberExpression)current;
        }

        private static Expression? AsDateTime(Expression member)
        {
            var type = member.Type;
            if (type == typeof(DateTime))
                return member;
            var underlying = Nullable.GetUnderlyingType(type);
            if (underlying == typeof(DateTime))
                return Expression.Property(member, "Value");
            return null;
        }
    }
}
