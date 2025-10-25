using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TransferCenterCore.Interfaces;
using TransferCenterWeb.Models;

namespace TransferCenterWeb.Controllers;

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
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "GlobalPatientTransfer");
        }
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
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.LoginId),
                new Claim(ClaimTypes.Email, user.EmailId),
                new Claim("Role", user.Role.ToString()) // Role claim for policy-based auth
            };

            var claimsIdentity = new ClaimsIdentity(claims, "TransferCenter");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync("TransferCenter", claimsPrincipal, new AuthenticationProperties
            {
                IsPersistent = model.RememberMe,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
            });

            return RedirectToAction("Index", "GlobalPatientTransfer");
        }

        ModelState.AddModelError(string.Empty, Constant.Log.Error.InvalidLoginAttempt);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("TransferCenter");
        return RedirectToAction("Login");
    }
}