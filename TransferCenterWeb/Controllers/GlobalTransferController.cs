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
        public ActionResult Index()
        {
            return View();
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
                await _globalTransferService.Save(patientTransferViewModel.ToCoreModel());
                return View(patientTransferViewModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
