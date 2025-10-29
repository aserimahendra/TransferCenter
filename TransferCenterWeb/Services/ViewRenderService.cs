using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace TransferCenterWeb;

public sealed class ViewRenderService : IViewRenderService
{
    private readonly IRazorViewEngine _viewEngine;
    private readonly ITempDataProvider _tempDataProvider;
    private readonly IModelMetadataProvider _modelMetadataProvider;

    public ViewRenderService(
        IRazorViewEngine viewEngine,
        ITempDataProvider tempDataProvider,
        IModelMetadataProvider modelMetadataProvider)
    {
        _viewEngine = viewEngine ?? throw new ArgumentNullException(nameof(viewEngine));
        _tempDataProvider = tempDataProvider ?? throw new ArgumentNullException(nameof(tempDataProvider));
        _modelMetadataProvider = modelMetadataProvider ?? throw new ArgumentNullException(nameof(modelMetadataProvider));
    }

    public async Task<string> RenderToStringAsync(ActionContext actionContext, string viewName, object model, bool isPartial = false)
    {
        ArgumentNullException.ThrowIfNull(actionContext);
        if (string.IsNullOrWhiteSpace(viewName))
        {
            throw new ArgumentException("View name must be provided.", nameof(viewName));
        }

        var viewResult = FindView(actionContext, viewName, isPartial);
        if (!viewResult.Success)
        {
            throw new InvalidOperationException($"View '{viewName}' was not found.");
        }

        await using var writer = new StringWriter();
        var viewData = new ViewDataDictionary(_modelMetadataProvider, actionContext.ModelState)
        {
            Model = model
        };

        var tempData = new TempDataDictionary(actionContext.HttpContext, _tempDataProvider);

        var viewContext = new ViewContext(
            actionContext,
            viewResult.View,
            viewData,
            tempData,
            writer,
            new HtmlHelperOptions());

        await viewResult.View.RenderAsync(viewContext).ConfigureAwait(false);
        return writer.ToString();
    }

    private ViewEngineResult FindView(ActionContext actionContext, string viewName, bool isPartial)
    {
        var result = _viewEngine.FindView(actionContext, viewName, !isPartial);
        if (result.Success)
        {
            return result;
        }

        return _viewEngine.GetView(executingFilePath: null, viewPath: viewName, isMainPage: !isPartial);
    }
}
