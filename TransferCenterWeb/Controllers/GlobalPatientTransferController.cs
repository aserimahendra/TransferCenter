using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransferCenterCore.Interfaces;
using TransferCenterWeb.Models.GlobalPatientTransfer;
using TransferCenterWeb.Models;
using TransferCenterWeb.Translators;
using TransferCenterHelper;
using TransferCenterWeb.Utility;

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
    public async Task<IActionResult> TransferList(string? caseMgr, string? patientName, DateTime? transferFrom, DateTime? transferTo, int page = 1, int pageSize = 10)
    {
        page = page < 1 ? 1 : page;
        pageSize = pageSize <= 0 ? 10 : pageSize;
        DateTime? from = transferFrom?.Date;
        DateTime? to = transferTo?.Date;
        if (from.HasValue && to.HasValue && from > to)
            (from, to) = (to, from);

        var (items, totalCount) = await _globalTransferService.GetList(page, pageSize, caseMgr, from, to, patientName);
        var webItems = items.Select(x => x.ToWebModel()).ToList();

        var viewModel = new GlobalPatientTransferListViewModel
        {
            Items = webItems,
            TotalCount = totalCount,
            CaseMgrSwRn = caseMgr,
            PatientName = patientName,
            TransferDateFrom = from,
            TransferDateTo = to
        };

        return PartialView("PatientTransferList", viewModel);
    }
    
    [HttpGet]
    public async Task<IActionResult> ExportToExcel(string? caseManager = null, string? name = null, DateTime? transferDateFrom = null, DateTime? transferDateTo = null)
    {
        try
        {
            var (items, _) = await _globalTransferService.GetList(caseManager, transferDateFrom, transferDateTo, name);
            var webItems = items.Select(x => x.ToWebModel()).ToList();
            var sheets = new Dictionary<string, (Type, System.Collections.IEnumerable)>
            {
                { "Transfer Info", (typeof(TransferCenterWeb.Models.PatientTransfer.PatientTransferInfo), webItems.Select(x => x.PatientTransferInfo).Where(x => x != null).ToList()) },
                { "Patient Details", (typeof(TransferCenterWeb.Models.PatientTransfer.PatientDetails), webItems.Select(x => x.PatientDetails).Where(x => x != null).ToList()) },
                { "Additional Info", (typeof(TransferCenterWeb.Models.PatientTransfer.AdditionalInfo), webItems.Select(x => x.AdditionalInfo).Where(x => x != null).ToList()) },
            };
            var excludeFieldsSetting = HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;
            var excludeFields = excludeFieldsSetting.GetExcelExportExcludeFields(Constant.Config.ExcelExportExcludeFields);
            var excelBytes = TransferCenterWeb.Utility.ExcelExportHelper.ExportToExcel(sheets, excludeFields);
            var fileName = $"GlobalPatientTransfers_{DateTime.UtcNow:yyyyMMdd}.xlsx";
            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        catch (Exception ex)
        {
            throw ex;
        }
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