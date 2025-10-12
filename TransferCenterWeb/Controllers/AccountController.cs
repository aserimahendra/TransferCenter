using Microsoft.AspNetCore.Mvc;
using TransferCenterBusinessInterface;
using TransferCenterWeb.Models;

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
                HttpContext.Session.SetInt32(Constant.Session.IsAuthenticated, 1);
                HttpContext.Session.SetString(Constant.Session.UserId, user.LoginId);
                HttpContext.Session.SetString(Constant.Session.Email, user.EmailId);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, Constant.Log.Error.InvalidLoginAttempt);
            return View(model);
        }
    }
}
