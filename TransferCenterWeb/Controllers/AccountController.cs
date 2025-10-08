using Microsoft.AspNetCore.Mvc;
using TransferCenterWeb.Models;
using TransferCenterModel;
using TransferCenterBusinessInterface;

namespace TransferCenterWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserBusiness _userBusiness;
        public AccountController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userBusiness.Login(model.LoginId, model.Password);
            if (user != null)
            {
                // TODO: Implement authentication logic (cookie, claims, etc.)
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }
    }
}
