using Microsoft.AspNetCore.Mvc;
using TransferCenterCore.Interface;
using TransferCenterWeb.Models;
using TransferCenterWeb.Translators;

namespace TransferCenterWeb.Controllers
{
    public class GlobalTransferController : Controller
    {
        IGlobalTransferService _globalTransferService;
        public GlobalTransferController(IGlobalTransferService globalTransferService)
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
            PatientTransferViewModel patientTransferViewModel = new PatientTransferViewModel();
            return View(patientTransferViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PatientTransferViewModel patientTransferViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(patientTransferViewModel);
                patientTransferViewModel.Id= Guid.NewGuid();
                
                await _globalTransferService.Save(patientTransferViewModel.ToCoreModel());
                return View("index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var details = await _globalTransferService.Get(id);
            return View(details.ToWebModel());
        }

    }
}
