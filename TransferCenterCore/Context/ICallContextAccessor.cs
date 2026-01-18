using System;

namespace TransferCenterCore.Context;

public interface ICallContextAccessor
{
	CallContext Current { get; }
	IDisposable BeginScope(CallContext context);
}
