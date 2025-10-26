using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransferCenterCore.Interfaces;
using TransferCenterWeb.Models;
using TransferCenterWeb.Models.PatientTransfer;
using TransferCenterWeb.Translators;

namespace TransferCenterWeb.Controllers;

[Authorize]
public class PatientTransferController : Controller
{
    private readonly IPatientTransferService _patientTransferService;

    public PatientTransferController(IPatientTransferService patientTransferService)
    {
        _patientTransferService = patientTransferService;
    }

    // GET: PatientTransferController
    public async Task<IActionResult> Index()
    {
        var transferRequests = await _patientTransferService.GetList();
        var viewModel = transferRequests?.Select(x => x.ToWebModel()).ToList() ?? new List<PatientTransferRequest>(); 
        return View(viewModel);
    }

    public IActionResult Create()
    {
        var patientTransferRequest = new PatientTransferRequest();
        return View(patientTransferRequest);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PatientTransferRequest patientTransferRequest)
    {
        if (!ModelState.IsValid)
            return View(patientTransferRequest);

        patientTransferRequest.Id = Guid.NewGuid();
        await _patientTransferService.Save(patientTransferRequest.ToCoreModel());
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var details = await _patientTransferService.Get(id);
        if (details == null)
            return NotFound();

        return View(details.ToWebModel());
    }
}