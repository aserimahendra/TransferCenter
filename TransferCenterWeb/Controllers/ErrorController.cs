using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TransferCenterWeb.Controllers;

[Authorize]
public class ErrorController : Controller
{
    [Route("Error/{statusCode?}")]
    public IActionResult Index(int? statusCode = null)
    {
        Response.StatusCode = statusCode ?? 500;
        ViewBag.ErrorMessage = "An error occurred while processing your request. Please contact your administrator.";
        ViewBag.StatusCode = statusCode;
        return View();
    }

    [Route("Error/AccessDenied")]
    public IActionResult AccessDenied()
    {
        Response.StatusCode = 403;
        ViewBag.ErrorMessage = "You do not have permission to access this resource.";
        return View("Index");
    }

    [Route("Error/NotAuthenticated")]
    public IActionResult NotAuthenticated()
    {
        Response.StatusCode = 401;
        ViewBag.ErrorMessage = "You must be logged in to access this resource.";
        return View("Index");
    }
}