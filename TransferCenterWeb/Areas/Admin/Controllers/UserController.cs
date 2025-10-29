using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransferCenterCore.Interfaces;
using TransferCenterWeb.Areas.Admin.Models;
using TransferCenterWeb.Models;
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
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> UserTable(int page = 1, int pageSize = 10)
    {
        var viewModel = await BuildUserViewModel(page, pageSize);
        return PartialView("_UserTable", viewModel);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var model = new User
        {
            Role = (short)RoleType.Admin,
            IsActive = true,
            CreatedOn = DateTime.UtcNow
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(User model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        model.Role = (short)RoleType.Admin;
        if (model.CreatedOn == DateTime.MinValue)
        {
            model.CreatedOn = DateTime.UtcNow;
        }

        try
        {
            model.IsActive = true;
            _userService.SaveUser(model.ToCoreModel());

            var successResult = new ModalActionResult(
                Constant.Status.Message.Created,
                Constant.Status.Code.Success,
                true);

            return PartialView(Constant.ViewPath.ModalActionResult, successResult);
        }
        catch (Exception ex)
        {
            var errorResult = new ModalActionResult(
                string.IsNullOrWhiteSpace(ex.Message) ? "Internal server error." : ex.Message,
                Constant.Status.Code.Error,
                false);

            return PartialView(Constant.ViewPath.ModalActionResult, errorResult);
        }
    }

    [HttpGet]
    public IActionResult Edit(long id)
    {
        var coreUser = _userService.GetUserById((int)id);
        if (coreUser == null)
        {
            return NotFound();
        }

        var model = coreUser.ToWebModel();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(User model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        model.Role = (short)RoleType.Admin;
        if (model.CreatedOn == DateTime.MinValue)
        {
            model.CreatedOn = DateTime.UtcNow;
        }

        try
        {
            _userService.UpdateUser(model.ToCoreModel());

            var successResult = new ModalActionResult(
                Constant.Status.Message.Updated,
                Constant.Status.Code.Success,
                true);

            return PartialView(Constant.ViewPath.ModalActionResult, successResult);
        }
        catch (Exception ex)
        {
            var errorResult = new ModalActionResult(
                string.IsNullOrWhiteSpace(ex.Message) ? "Internal server error." : ex.Message,
                Constant.Status.Code.Error,
                false);

            return PartialView(Constant.ViewPath.ModalActionResult, errorResult);
        }
    }

    private async Task<UserViewModel> BuildUserViewModel(int page, int pageSize)
    {
        var response = await _userService.GetAllUsersAsync(page, pageSize);
        return new UserViewModel
        {
            Users = response.Users.ToWebModel().ToList(),
            TotalCount = response.TotalCount
        };
    }
}