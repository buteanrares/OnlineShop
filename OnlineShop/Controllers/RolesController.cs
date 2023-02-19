using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Controllers
{
    public class RolesController : Controller
    {
        private RoleManager<IdentityRole> RoleManager;
        private UserManager<UserAccount> UserManager;

        public RolesController(RoleManager<IdentityRole> roleMgr, UserManager<UserAccount> userManager)
        {
            RoleManager = roleMgr;
            this.UserManager = userManager;
        }
        public ViewResult Index() => View(RoleManager.Roles);

        public IActionResult Create() => View();


        [HttpPost]
        public async Task<IActionResult> Create([Required] string name)
        {
            if (ModelState.IsValid)
            {
                var result = await RoleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await RoleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                Errors(result);
            }
            else
                ModelState.AddModelError("", "No role found");
            return View("Index", RoleManager.Roles);
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

        public async Task<IActionResult> Update(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            List<UserAccount> members = new();
            List<UserAccount> nonMembers = new();
            foreach (var user in UserManager.Users)
            {
                var list = await UserManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
            }
            return View(new RoleEdit
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }


        [HttpPost]
        public async Task<IActionResult> Update(RoleModification model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.AddIds ?? new string[] { })
                {
                    var user = await UserManager.FindByIdAsync(userId);
                    if (user == null) continue;
                    result = await UserManager.AddToRoleAsync(user, model.RoleName);
                    if (!result.Succeeded)
                        Errors(result);
                }
                foreach (string userId in model.DeleteIds ?? new string[] { })
                {
                    var user = await UserManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await UserManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            Errors(result);
                    }
                }
            }
            if (ModelState.IsValid)
                return RedirectToAction(nameof(Index));
            else
                return await Update(model.RoleId);
        }
    }

}
