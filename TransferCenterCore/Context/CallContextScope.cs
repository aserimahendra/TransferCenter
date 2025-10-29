using System;

namespace TransferCenterCore.Context;

/// <summary>
/// Provides a simple ambient scope for <see cref="CallContext"/> so callers do not need to depend on DI.
/// </summary>
public static class CallContextScope
{
    /// <summary>
    /// Gets the current call context or returns the empty context when none is present.
    /// </summary>
    public static CallContext Current => AmbientContext<CallContext>.Current ?? CallContext.Empty;

    /// <summary>
    /// Enters a new ambient scope for the provided <paramref name="context"/>.
    /// </summary>
    public static IDisposable Begin(CallContext? context)
    {
        return AmbientContext<CallContext>.Enter(context ?? CallContext.Empty);
    }
}
