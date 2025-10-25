using Microsoft.AspNetCore.Mvc;

namespace TransferCenterWeb.Areas.Admin.Controllers;

public class User : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}