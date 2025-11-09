using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public async Task<IActionResult> UserList(string? search, int page = 1, int pageSize = 10)
    {
        var viewModel = await BuildUserViewModel(page, pageSize, search);
        return PartialView(viewModel);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new User());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(User model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        bool isDuplicate = _userService.CheckDuplicateEmailAndLogin(model.EmailId, model.LoginId, null);
        if (isDuplicate)
        {
            ModelState.AddModelError(nameof(model.EmailId), "Email ID or Login ID already exists.");
            return View(model);
        }
        try
        {
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
        var coreUser = _userService.GetUserById(id);
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
        bool isDuplicate = _userService.CheckDuplicateEmailAndLogin(model.EmailId, model.LoginId, model.UserId);
        if (isDuplicate)
        {
            ModelState.AddModelError(nameof(model.EmailId), "Email ID or Login ID already exists.");
            return View(model);
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

    private async Task<UserViewModel> BuildUserViewModel(int page, int pageSize, string? search = null)
    {
        var response = await _userService.GetAllUsersAsync(page, pageSize, search);
        return new UserViewModel
        {
            Users = response.Users.ToWebModel().ToList(),
            TotalCount = response.TotalCount
        };
    }

    [HttpPut]
    [ValidateAntiForgeryToken]
    public IActionResult ToggleActive(long id, bool isActive)
    {
        var coreUser = _userService.GetUserById(id);
        if (coreUser == null)
            return NotFound(new { success = false, message = "User not found" });

        coreUser.IsActive = isActive;
        _userService.UpdateUser(coreUser);
        return Json(new { success = true, isActive });
    }
}