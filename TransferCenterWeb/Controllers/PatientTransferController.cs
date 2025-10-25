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
        var infoDList = await _patientTransferService.GetList();
        return View(infoDList.Select(x => x.ToWebModel()).ToList());
    }

    public IActionResult Create()
    {
        PatientTransferRequest patientTransferRequest = new PatientTransferRequest();
        return View(patientTransferRequest);
    }

    [HttpPost]
    public async Task<IActionResult> Create(PatientTransferRequest patientTransferRequest)
    {
        if (!ModelState.IsValid)
            return View(patientTransferRequest);

        patientTransferRequest.Id = Guid.NewGuid();
        await _patientTransferService.Save(patientTransferRequest.ToCoreModel());
        return View();
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var details = await _patientTransferService.Get(id);
        return View(details.ToWebModel());
    }
}