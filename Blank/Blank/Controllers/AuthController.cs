using Blank.Models;
using Blank.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blank.Controllers
{
	public class AuthController : Controller
	{
		private SignInManager<BlankUser> signInManager;

		public AuthController(SignInManager<BlankUser> signInManager)
		{
			this.signInManager = signInManager;
		}

		public IActionResult Login()
		{
			if (User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Trips", "App");
			}

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel vm, string returlUrl)
		{
			if (ModelState.IsValid)
			{
				var signInResult = await signInManager.PasswordSignInAsync(vm.Username, vm.Password, false, false);

				if (signInResult.Succeeded)
				{
					return string.IsNullOrWhiteSpace(returlUrl) ? (ActionResult)RedirectToAction("Trips", "App") : Redirect(returlUrl);
				}
				else
					ModelState.AddModelError("", "Username or password incorrect");
			}
			return View();
		}

		public async Task<ActionResult> Logout()
		{
			if (User.Identity.IsAuthenticated)
				await signInManager.SignOutAsync();

			return RedirectToAction("Index", "App");
		}
	}
}
