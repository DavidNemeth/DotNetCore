using Blank.Services;
using Blank.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Blank.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService mailService;
        private IConfigurationRoot _config;

        public AppController(IMailService mailService, IConfigurationRoot config)
        {
            this.mailService = mailService;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (model.Email.Contains("aol.com"))
                ModelState.AddModelError("Email", "We don`t support AOL addresses");

            if (ModelState.IsValid)
            {
                mailService.SendMail(_config["MailSettings:ToAddress"], model.Email, "from app Blank", model.Message);

                ModelState.Clear();
                ViewBag.UserMessage = "Message sent";
            }

            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
