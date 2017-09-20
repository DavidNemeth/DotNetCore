using Blank.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Blank.Controllers.Web
{
    public class AppController : Controller
    {
        public IActionResult GetIndex()
        {
            return View();
        }

        public IActionResult GetContact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            return View();
        }

        public IActionResult GetAbout()
        {
            return View();
        }
    }
}
