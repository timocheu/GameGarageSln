using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GameGarage.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace GameGarage.Controllers
{
    [Authorize] // Ensure access is restricted
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly GameGarage.Infrastructure.IAuditService _auditService;

        public UsersController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, GameGarage.Infrastructure.IAuditService auditService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _auditService = auditService;
        }

        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();
            var userViewModels = users.Select(user => new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            }).ToList();

            return View(userViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id!);
                if (user == null)
                {
                    return NotFound();
                }

                user.Email = model.Email;
                user.UserName = model.UserName;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    // If the updated user is the current user, refresh the sign-in cookie
                    var currentUser = await _userManager.GetUserAsync(User);
                    if (currentUser != null && currentUser.Id == user.Id)
                    {
                        await _signInManager.RefreshSignInAsync(user);
                    }

                    await _auditService.LogAction(User.Identity?.Name ?? "Admin", "Edit User", $"Updated profile for user '{user.UserName}' (ID: {user.Id})");

                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userName = user.UserName;
            var userId = user.Id;
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                await _auditService.LogAction(User.Identity?.Name ?? "Admin", "Delete User", $"Deleted user account '{userName}' (ID: {userId})");
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("Index", _userManager.Users.ToList());
        }
    }
}
