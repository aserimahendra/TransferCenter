using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using TransferCenterCore.Interfaces;
using TransferCenterHelper;
using TransferCenterWeb.Models;
using TransferCenterWeb.Models.PatientTransfer;
using TransferCenterWeb.Models.ViewModel;
using TransferCenterWeb.Translators;
using TransferCenterHelper.Utility;
using TransferCenterWeb.Utility;

namespace TransferCenterWeb.Controllers;

[Authorize]
public class PatientTransferController : Controller
{

    private readonly IPatientTransferService _patientTransferService;
    private readonly IPdfExporter _pdfExporter;
    private readonly IViewRenderService _viewRenderService;

    public PatientTransferController(
        IPatientTransferService patientTransferService,
        IPdfExporter pdfExporter,
        ITempDataProvider tempDataProvider,
        IViewRenderService viewRenderService)
    {
        _patientTransferService = patientTransferService;
        _pdfExporter = pdfExporter;
        _viewRenderService = viewRenderService;
    }

    // GET: PatientTransferController
    public async Task<IActionResult> Index()
    { 
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> PatientTransferList(int page = 1, int pageSize = 10, string? caseManager = null, string? name = null, DateTime? transferDateFrom = null, DateTime? transferDateTo = null)
    {
        page = page < 1 ? 1 : page;
        pageSize = pageSize <= 0 ? 10 : pageSize;

        // Validate date range
        var (isValid, errorMessage) = TransferCenterWeb.Extensions.ExportValidationExtensions.ValidateFilterDateRange(transferDateFrom, transferDateTo);
        if (!isValid)
        {
            var viewModel = new PatientTransferListViewModel()
            {
                Items = new List<PatientTransferRequest>(),
                TotalCount = 0,
                Name = name,
                CaseManager = caseManager,
                TransferDateFrom = transferDateFrom,
                TransferDateTo = transferDateTo,
                ErrorMessage = errorMessage
            };
            return PartialView("PatientTransferList", viewModel);
        }

        var (items, totalCount) = await _patientTransferService.GetList(page, pageSize, caseManager, transferDateFrom, transferDateTo, name);
        var webItems = items.Select(x => x.ToWebModel()).ToList();
        var validViewModel = new PatientTransferListViewModel()
        {
            Items = webItems,
            TotalCount = totalCount,
            Name = name,
            CaseManager = caseManager,
            TransferDateFrom = transferDateFrom,
            TransferDateTo = transferDateTo,
            ErrorMessage = null
        };
        return PartialView("PatientTransferList", validViewModel);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var patientTransferRequest = new PatientTransferRequest();
        return View(patientTransferRequest);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PatientTransferRequest patientTransferRequest)
    {
        var error = ModelState.Values.SelectMany(x => x.Errors).ToList();
        if (!ModelState.IsValid)
            return View(patientTransferRequest);

        patientTransferRequest.Id = Guid.NewGuid();
        await _patientTransferService.Save(patientTransferRequest.ToCoreModel());

        var result = new ModalActionResult(
            Constant.Status.Message.Created,
            Constant.Status.Code.Success);

       return PartialView(Constant.ViewPath.ModalActionResult, result);
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var details = await _patientTransferService.Get(id);
        if (details == null)
            return NotFound();

        return View(details.ToWebModel());
    }
    
    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        var details = await _patientTransferService.Get(id);
        if (details == null) return NotFound();
        var patientTransferRequest = details.ToWebModel();
        return View(patientTransferRequest);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(PatientTransferRequest patientTransferRequest)
    { 
        if (!ModelState.IsValid)
            return View(patientTransferRequest);

        try
        {
            await _patientTransferService.Update(patientTransferRequest.ToCoreModel());

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

    [HttpGet]
    [Authorize(Policy = "RequireAdminRole")]
    public IActionResult DeleteConformation(Guid id, string msg)
    {
        if (id == Guid.Empty) return NotFound();
        return View(new DeleteViewModel(){Uid = id, Message = msg});
    }
    
    
    [HttpPost]
    [Authorize(Policy = "RequireAdminRole")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var details = await _patientTransferService.Get(id);
            await _patientTransferService.Delete(details);
            var successResult = new ModalActionResult(
                Constant.Status.Message.Deleted,
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
            var details = await _patientTransferService.Get(id);
            if (details == null)
                return NotFound();
    
         var model = details.ToWebModel();
         var htmlContent = await _viewRenderService.RenderToStringAsync(ControllerContext, "Details", model);
        var baseUrl = $"{Request.Scheme}://{Request.Host}";
         var pdfBytes = _pdfExporter.ConvertHtmlToPdf(htmlContent, $"In-Patient Transfer Details: {details.PatientTransferInfo?.CaseMgrSwRn}", baseUrl);
        var safeFileName = $"PatientTransfer_{details.PatientTransferInfo?.CaseMgrSwRn ?? string.Empty}{Guid.NewGuid().ToString("N")}.pdf";
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
    #region Export to Excel

    [HttpGet]
    public async Task<IActionResult> ExportToExcel(string? caseManager = null, string? name = null, DateTime? transferDateFrom = null, DateTime? transferDateTo = null)
    {
        try
        {
            //Validate date range (max 31 days)
            var (isValid, errorMessage) = TransferCenterWeb.Extensions.ExportValidationExtensions.ValidateExportDateRange(transferDateFrom, transferDateTo);
            if (!isValid)
            {
                var errorResult = new ModalActionResult(
                    errorMessage,
                    Constant.Status.Code.Error,
                    false);
                return PartialView(Constant.ViewPath.ModalActionResult, errorResult);
            }

            var (items, _) = await _patientTransferService.GetList(caseManager, transferDateFrom, transferDateTo, name);
            var webItems = items.Select(x => x.ToWebModel()).ToList();
            var sheets = new Dictionary<string, (Type, System.Collections.IEnumerable)>
            {
                { "Transfer Info", (typeof(TransferCenterWeb.Models.PatientTransfer.PatientTransferInfo), webItems.Select(x => x.PatientTransferInfo).Where(x => x != null).ToList()) },
                { "Patient Details", (typeof(TransferCenterWeb.Models.PatientTransfer.PatientDetails), webItems.Select(x => x.PatientDetails).Where(x => x != null).ToList()) },
                { "Additional Info", (typeof(TransferCenterWeb.Models.PatientTransfer.AdditionalInfo), webItems.Select(x => x.AdditionalInfo).Where(x => x != null).ToList()) },
                { "Comorbidities", (typeof(TransferCenterWeb.Models.PatientTransfer.ComorbiditiesAndRiskScore), webItems.Select(x => x.ComorbiditiesAndRiskScore).Where(x => x != null).ToList()) }
            };
            var excludeFieldsSetting = HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;
            var excludeFields = excludeFieldsSetting.GetExcelExportExcludeFields(Constant.Config.ExcelExportExcludeFields);
            var excelBytes = await Task.Run(() => ExcelExportHelper.ExportToExcel(sheets, excludeFields));
            var fileName = $"PatientTransfers_{DateTime.UtcNow:yyyyMMdd}.xlsx";
            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
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
    
    #endregion

    
}