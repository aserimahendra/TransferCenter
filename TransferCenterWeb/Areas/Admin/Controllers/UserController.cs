using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransferCenterCore.Interfaces;
using TransferCenterWeb.Areas.Admin.Models;
using TransferCenterWeb.Translators;

namespace TransferCenterWeb.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Policy = "RequireAdminRole")]
public class UserController : Controller
{
    IUserService _userService;
    public UserController(IUserService  userService)
    {
        _userService = userService;
    }
    // GET
    public async Task<IActionResult> Index(int page=1, int pageSize=10)
    {
        var response = await _userService.GetAllUsersAsync(page, pageSize);
        ViewBag.TotalCount = response.TotalCount;
        UserViewModel userViewModel = new UserViewModel()
        {
            Users = response.Users.ToWebModel().ToList(),
            TotalCount = response.TotalCount
        };
        return View(userViewModel);
    }
}