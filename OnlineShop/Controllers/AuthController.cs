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
        public UserManager<UserAccount> UserManager { get; }
        public SignInManager<UserAccount> SignInManager { get; }


        public AuthController(UserManager<UserAccount> userManager, SignInManager<UserAccount> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        // GET: RegisterController
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public async Task<ActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return Redirect("./Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("UserName,FirstName,LastName,DateOfBirth,Password,ConfirmPassword")] RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new Customer()
                {
                    UserName = registerViewModel.UserName,
                    Email = registerViewModel.UserName,
                    FirstName= registerViewModel.FirstName,
                    LastName= registerViewModel.LastName,
                    DateOfBirth = registerViewModel.DateOfBirth,
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("UserName,Password,RememberMe")] LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await SignInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, loginViewModel.RememberMe, false);

                if(loginResult.Succeeded)
                {
                    return Redirect("/../../Home");
                }

                ModelState.AddModelError(string.Empty, "Inexistent account or incorrect credentials.");
            }
            return View();
        }
    }
}
