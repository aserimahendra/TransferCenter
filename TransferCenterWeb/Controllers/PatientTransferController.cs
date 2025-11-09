using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using TransferCenterCore.Interfaces;
using TransferCenterHelper;
using TransferCenterWeb.Models;
using TransferCenterWeb.Models.PatientTransfer;
using TransferCenterWeb.Models.ViewModel;
using TransferCenterWeb.Translators;

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
    public async Task<IActionResult> PatientTransferList(int page = 1, int pageSize = 10, string? caseManager = null, DateTime? transferDateFrom = null, DateTime? transferDateTo = null)
    {
        page = page < 1 ? 1 : page;
        pageSize = pageSize <= 0 ? 10 : pageSize;
        var (items, totalCount) = await _patientTransferService.GetList(page, pageSize, caseManager, transferDateFrom, transferDateTo);
        var webItems = items.Select(x => x.ToWebModel()).ToList();
        var viewModel = new PatientTransferListViewModel()
        {
            Items = webItems,
            TotalCount = totalCount,
            CaseManager = caseManager,
            TransferDateFrom = transferDateFrom,
            TransferDateTo = transferDateTo
        };
        return PartialView("PatientTransferList", viewModel);
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
    
}