using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SkeletaDAL.Core.CoreModel;
using SkeletaWeb.ViewModels;
using System.Threading.Tasks;

namespace SkeletaWeb.Controllers
{
	public class AuthorizationController : Controller
	{
		private IOptions<IdentityOptions> identityOptions;
		private SignInManager<ApplicationUser> signInManager;
		private UserManager<ApplicationUser> userManager;

		public AuthorizationController(
			IOptions<IdentityOptions> identityOptions,
			SignInManager<ApplicationUser> signInManager,
			UserManager<ApplicationUser> userManager)
		{
			this.identityOptions = identityOptions;
			this.signInManager = signInManager;
			this.userManager = userManager;
		}

		public IActionResult Login()
		{
			if (User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Index", "Customers");
			}

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel vm, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				var signInResult = await signInManager.PasswordSignInAsync(vm.Username, vm.Password, true, false);

				if (signInResult.Succeeded)
					return string.IsNullOrWhiteSpace(returnUrl) ? (ActionResult)RedirectToAction("index", "customers") : Redirect(returnUrl);
				else
					ModelState.AddModelError("", "Username or password incorrect");

			}

			return View();
		}

		public async Task<IActionResult> Logout()
		{
			if (User.Identity.IsAuthenticated)
				await signInManager.SignOutAsync();

			return RedirectToAction("Index", "Home");
		}
	}
}
