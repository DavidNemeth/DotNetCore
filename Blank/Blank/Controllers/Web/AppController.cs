using Blank.DAL;
using Blank.DAL.Interfaces;
using Blank.Services;
using Blank.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Blank.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService mailService;
        private IConfigurationRoot config;
        private IBlankRepository repo;

        public AppController(IMailService mailService, IConfigurationRoot config, BlankRepository repo)
        {
            this.mailService = mailService;
            this.config = config;
            this.repo = repo;
        }

        public IActionResult Index()
        {
            var data = repo.GetAllTrip();

            return View(data);
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
                mailService.SendMail(config["MailSettings:ToAddress"], model.Email, "from app Blank", model.Message);

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
