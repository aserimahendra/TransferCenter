using Microsoft.AspNetCore.Mvc;
using TransferCenterCore.Interface;
using TransferCenterWeb.Models;

namespace TransferCenterWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
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

            var user = await _userService.Login(model.LoginId, model.Password);
            if (user != null)
            {
                // TODO: Implement authentication logic (cookie, claims, etc.)
                HttpContext.Session.SetInt32(Constant.Session.IsAuthenticated, 1);
                HttpContext.Session.SetString(Constant.Session.UserId, user.LoginId);
                HttpContext.Session.SetString(Constant.Session.Email, user.EmailId);
                return RedirectToAction("Index", "GlobalTransfer");
            }
            ModelState.AddModelError(string.Empty, Constant.Log.Error.InvalidLoginAttempt);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return View("login");
        }
    }
}
