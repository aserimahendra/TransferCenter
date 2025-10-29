using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using TransferCenterCore.Interfaces;
using TransferCenterWeb.Models.GlobalPatientTransfer;
using TransferCenterWeb.Models;
using TransferCenterWeb.Translators;
using TransferCenterHelper;

namespace TransferCenterWeb.Controllers;

[Authorize]
public class GlobalPatientTransferController : Controller
{
    IGlobalTransferService _globalTransferService;
     private readonly IPdfExporter _pdfExporter;
    private readonly IViewRenderService _viewRenderService;

    public GlobalPatientTransferController(
        IGlobalTransferService globalTransferService,
        IPdfExporter pdfExporter,
        IViewRenderService viewRenderService)
    {
        _globalTransferService = globalTransferService;
        _pdfExporter = pdfExporter;
        _viewRenderService = viewRenderService;
    }
    // GET: GlobalTransferController
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> TransferList(int page = 1, int pageSize = 10)
    {
        page = page < 1 ? 1 : page;
        pageSize = pageSize <= 0 ? 10 : pageSize;

        var (items, totalCount) = await _globalTransferService.GetList(page, pageSize);
        var webItems = items.Select(x => x.ToWebModel()).ToList();

        var viewModel = new GlobalPatientTransferListViewModel
        {
            Items = webItems,
            TotalCount = totalCount
        };

        return PartialView("PatientTransferList", viewModel);
    }


    public IActionResult Create()
    {
        GlobalPatientTransferRequest globalPatientTransferRequest = new GlobalPatientTransferRequest();
        return View(globalPatientTransferRequest);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(GlobalPatientTransferRequest globalPatientTransferRequest)
    {
        if (!ModelState.IsValid)
            return View(globalPatientTransferRequest);

        try
        {
            globalPatientTransferRequest.Id = Guid.NewGuid();
            await _globalTransferService.Save(globalPatientTransferRequest.ToCoreModel());

            var successResult = new ModalActionResult(
                Constant.Status.Message.Created,
                Constant.Status.Code.Success,
                true);

            return PartialView(Constant.ViewPath.ModalActionResult, successResult);
        }
        catch (Exception ex)
        {
            var errorResult = new ModalActionResult(
                string.IsNullOrWhiteSpace(ex.Message) ? "Internal server error." : ex.Message,
                Constant.Status.Code.Error,
                false);

            return PartialView(Constant.ViewPath.ModalActionResult, errorResult);
        }
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var details = await _globalTransferService.Get(id);
        return View(details.ToWebModel());
    }

    public async Task<IActionResult> Update(Guid id)
    {
        var details = await _globalTransferService.Get(id);
        if (details == null) return NotFound();
        var globalPatientTransferRequest = details.ToWebModel();
        return View(globalPatientTransferRequest);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(GlobalPatientTransferRequest globalPatientTransferRequest)
    {
        if (!ModelState.IsValid)
            return View(globalPatientTransferRequest);

        try
        {
            await _globalTransferService.Update(globalPatientTransferRequest.ToCoreModel());

            var successResult = new ModalActionResult(
                Constant.Status.Message.Updated,
                Constant.Status.Code.Success,
                true);

            return PartialView(Constant.ViewPath.ModalActionResult, successResult);
        }
        catch (Exception ex)
        {
            var errorResult = new ModalActionResult(
                string.IsNullOrWhiteSpace(ex.Message) ? "Internal server error." : ex.Message,
                Constant.Status.Code.Error,
                false);

            return PartialView(Constant.ViewPath.ModalActionResult, errorResult);
        }
    }
    public async Task<IActionResult> DetailsInPdf(Guid id)
    {
        try
        {
            var details = await _globalTransferService.Get(id);
            if (details == null)
                return NotFound();
            var model = details.ToWebModel();
            var htmlContent = await _viewRenderService.RenderToStringAsync(ControllerContext, "Details", model);
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            var pdfBytes = _pdfExporter.ConvertHtmlToPdf(htmlContent, $"Global Patient Transfer Details: {details.TransferInfo?.CaseMgrSwRn}", baseUrl);
            var safeFileName = $"GlobalPatientTransfer_{details.TransferInfo?.CaseMgrSwRn ?? string.Empty}{Guid.NewGuid().ToString("N")}.pdf";
            return File(pdfBytes, "application/pdf", safeFileName);
        }
        catch (Exception ex)
        {
            var errorResult = new ModalActionResult(
                string.IsNullOrWhiteSpace(ex.Message) ? "Internal server error." : ex.Message,
                Constant.Status.Code.Error,
                false);
    
            return PartialView(Constant.ViewPath.ModalActionResult, errorResult);
        }
    }
}