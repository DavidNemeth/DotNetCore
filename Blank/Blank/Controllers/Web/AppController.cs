using Blank.DAL.Interfaces;
using Blank.Services;
using Blank.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace Blank.Controllers.Web
{
    public class AppController : Controller
    {
        private ILogger<AppController> logger;
        private IMailService mailService;
        private IConfigurationRoot config;
        private IBlankRepository repo;

        public AppController(IMailService mailService, IConfigurationRoot config, IBlankRepository repo, ILogger<AppController> logger)
        {
            this.logger = logger;
            this.mailService = mailService;
            this.config = config;
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();

        }

        [Authorize]
        public IActionResult Trips()
        {
            try
            {
                var trips = repo.GetAllTrips();

                return View(trips);
            }
            catch (Exception ex)
            {

                logger.LogError($"Failed to get trips in Index page: {ex.Message}");
                return Redirect("/error");
            }

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
