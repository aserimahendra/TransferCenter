using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransferCenterCore.Interfaces;
using TransferCenterWeb.Models.GlobalPatientTransfer;
using TransferCenterWeb.Translators;

namespace TransferCenterWeb.Controllers;

[Authorize]
public class GlobalPatientTransferController : Controller
{
    IGlobalTransferService _globalTransferService;
    public GlobalPatientTransferController(IGlobalTransferService globalTransferService)
    {
        _globalTransferService = globalTransferService;
    }
    // GET: GlobalTransferController
    public async Task<IActionResult> Index()
    {
        var infoDList = await _globalTransferService.GetList();
        return View(infoDList.Select(x=>x.ToWebModel()).ToList());
    }


    public IActionResult Create()
    {
        GlobalPatientTransferRequest globalPatientTransferRequest = new GlobalPatientTransferRequest();
        return View(globalPatientTransferRequest);
    }

    [HttpPost]
    public async Task<IActionResult> Create(GlobalPatientTransferRequest globalPatientTransferRequest)
    {
        if (!ModelState.IsValid)
         return View(globalPatientTransferRequest);
        globalPatientTransferRequest.Id= Guid.NewGuid();
        await _globalTransferService.Save(globalPatientTransferRequest.ToCoreModel());
        return View();
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var details = await _globalTransferService.Get(id);
        return View(details.ToWebModel());
    }

}