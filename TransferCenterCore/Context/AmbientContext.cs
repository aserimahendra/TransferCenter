using System;
using System.Threading;

namespace TransferCenterCore.Context;

/// <summary>
/// Generic ambient context helper that stores a context object per async flow using AsyncLocal.
/// </summary>
/// <typeparam name="TContext">Context type to store.</typeparam>
public static class AmbientContext<TContext> where TContext : class
{
    private static readonly AsyncLocal<ContextHolder> Holder = new();

    public static TContext? Current => Holder.Value?.Value;

    public static IDisposable Enter(TContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var holder = Holder.Value;
        if (holder == null)
        {
            holder = new ContextHolder();
            Holder.Value = holder;
        }

        var previous = holder.Value;
        holder.Value = context;

        return new RevertAction(() => holder.Value = previous);
    }

    private sealed class ContextHolder
    {
        public TContext? Value { get; set; }
    }

    private sealed class RevertAction : IDisposable
    {
        private Action? _revert;

        public RevertAction(Action revert)
        {
            _revert = revert ?? throw new ArgumentNullException(nameof(revert));
        }

        public void Dispose()
        {
            _revert?.Invoke();
            _revert = null;
        }
    }
}
