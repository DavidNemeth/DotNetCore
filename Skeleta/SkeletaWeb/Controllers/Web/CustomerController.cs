using Microsoft.AspNetCore.Mvc;
using SkeletaDAL;
using SkeletaWeb.Services;
using System.Threading.Tasks;

namespace SkeletaWeb.Controllers.Web
{
	public class CustomerController : BaseController
	{
		public CustomerController(IUnitOfWork context, IServices services) : base(context, services)
		{
		}

		public async Task<IActionResult> IndexAsync()
		{
			var customers = await context.Customers.GetAllCustomerDataAsync();
			return View(customers);
		}
	}
}
