using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkeletaDAL;
using SkeletaWeb.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SkeletaWeb.Controllers
{
	public class HomeController : Controller
	{
		private readonly IUnitOfWork context;

		public HomeController(IUnitOfWork context)
		{
			this.context = context;
		}

		public async Task<IActionResult> Index()
		{
			var model = await context.Customers.GetAllCustomerDataAsync();
			var customers = Mapper.Map<List<CustomerViewModel>>(model);

			return View(customers);
		}

		public IActionResult Error()
		{
			ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
			return View();
		}
	}
}
