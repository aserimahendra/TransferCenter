using Microsoft.AspNetCore.Mvc;
using TransferCenterWeb.Models;

namespace TransferCenterWeb.Controllers
{
    public class GlobalTransferController : Controller
    {
        // GET: GlobalTransferController
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create()
        {
            PatientTransferViewModel patientTransferViewModel = new PatientTransferViewModel();
            return View(patientTransferViewModel);
        }

    }
}
