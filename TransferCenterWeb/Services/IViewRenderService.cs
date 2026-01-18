using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TransferCenterWeb;

public interface IViewRenderService
{
    Task<string> RenderToStringAsync(ActionContext actionContext, string viewName, object model, bool isPartial = false);
}
