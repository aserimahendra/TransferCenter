using Microsoft.AspNetCore.Mvc;

namespace TransferCenterWeb.Controllers;

public class PatientTransfer : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}