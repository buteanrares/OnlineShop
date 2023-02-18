using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Entities;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    public class AuthController : Controller
    {
        public UserManager<IdentityUser> UserManager { get; }
        public SignInManager<IdentityUser> SignInManager { get; }


        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        // GET: RegisterController
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Username,Password,ConfirmPassword")] RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    UserName = registerViewModel.Username,
                    Email = registerViewModel.Username
                };

                var result = await UserManager.CreateAsync(user, registerViewModel.Password);
                if(result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false);
                    return Redirect("/../../Home");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View();
        }
    }
}
