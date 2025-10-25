using System.Linq.Expressions;

namespace TransferCenterDbStore;

public interface IGenericRepository<T> where T : class
{
    void Add(T entity);
    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    void AddRange(IEnumerable<T> entities);
    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    T Get(Expression<Func<T, bool>> expression);
    Task<T> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);

    IEnumerable<T> GetAll();
    IEnumerable<T> GetAll(Expression<Func<T, bool>> expression);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);

    // IQueryable access for composing queries (deferred execution)
    IQueryable<T> Query();
    IQueryable<T> Query(Expression<Func<T, bool>> expression);

    // Count helpers
    Task<int> CountAsync(CancellationToken cancellationToken = default);
    Task<int> CountAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);

    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);

    void Update(T entity);
    void UpdateRange(IEnumerable<T> entities);
}