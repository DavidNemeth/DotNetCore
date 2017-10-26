using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkeletaDAL;
using SkeletaWeb.Services;
using SkeletaWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkeletaWeb.Controllers.APIs
{

	[Route("api/[controller]")]
	public class CustomerController : Controller
	{
		private readonly IUnitOfWork context;
		private readonly IServices services;
		private readonly ILogger logger;

		public CustomerController(IUnitOfWork context, IServices services, ILogger<CustomerController> logger)
		{
			this.context = context;
			this.services = services;
			this.logger = logger;
		}

		// GET api/customers
		[HttpGet("")]
		public async Task<IActionResult> GetCustomersAsync()
		{
			try
			{
				var customers = Mapper.Map<List<CustomerViewModel>>(await context.Customers.GetAllCustomerDataAsync());
				return Ok(customers);
			}
			catch (Exception ex)
			{
				logger.LogError($"failed to get all customers: {ex}");
				return BadRequest("Could not connect to Database");
			}
		}

		// GET api/customers/page/10/10
		[HttpGet("page/{skip}/{take}")]
		public async Task<ActionResult> GetCustomersPageAsync(int skip, int take)
		{
			try
			{
				var pagingResult = await context.Customers.GetCustomersPageAsync(skip, take);

				Response.Headers.Add("X-InlineCount", pagingResult.TotalRecords.ToString());
				return Ok(pagingResult.Records);
			}
			catch (Exception ex)
			{
				logger.LogError($"failed to load customers page list: {ex}");
				return BadRequest(new ApiResponse { Status = false });
				throw;
			}
		}
	}
}
