using System;

namespace TransferCenterCore.Context;

public class CallContextAccessor : ICallContextAccessor
{
	public CallContext Current => AmbientContext<CallContext>.Current ?? CallContext.Empty;

	public IDisposable BeginScope(CallContext context)
	{
		return AmbientContext<CallContext>.Enter(context ?? CallContext.Empty);
	}
}
