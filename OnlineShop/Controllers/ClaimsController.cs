using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using OnlineShop.Data.Entities;
using Microsoft.AspNetCore.Authorization;

namespace OnlineShop.Controllers
{
    [Authorize]
    public class ClaimsController : Controller
    {
        private UserManager<UserAccount> userManager;

        public ClaimsController(UserManager<UserAccount> userMgr)
        {
            userManager = userMgr;
        }

        public ViewResult Index() => View(User?.Claims);

        public ViewResult Create() => View();

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create_Post(string claimType, string claimValue)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            Claim claim = new Claim(claimType, claimValue, ClaimValueTypes.String);
            IdentityResult result = await userManager.AddClaimAsync(user, claim);
            if (result.Succeeded)
                return RedirectToAction("Index");
            Errors(result);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string claimValues)
        {
            var user = await userManager.GetUserAsync(HttpContext.User); string[] claimValuesArray = claimValues.Split(";");
            string claimType = claimValuesArray[0], claimValue = claimValuesArray[1], claimIssuer = claimValuesArray[2];
            var claim = User.Claims.Where(x => x.Type == claimType && x.Value == claimValue && x.Issuer == claimIssuer).FirstOrDefault();
            IdentityResult result = await userManager.RemoveClaimAsync(user, claim);
            if (result.Succeeded)
                return RedirectToAction("Index");
            Errors(result);
            return View("Index");
        }

        void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}
