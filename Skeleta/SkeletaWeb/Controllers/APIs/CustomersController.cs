using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkeletaDAL;
using SkeletaDAL.Models;
using SkeletaWeb.Services;
using SkeletaWeb.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkeletaWeb.Controllers.APIs
{
	[Produces("application/json")]
	[Route("api/customers")]
	public class CustomersController : BaseController
	{
		protected CustomersController(IUnitOfWork context, IServices services) : base(context, services)
		{
		}


		// GET: api/customers
		[HttpGet]
		public async Task<IEnumerable<CustomerViewModel>> GetCustomersAsync()
		{
			var customers = Mapper.Map<IEnumerable<CustomerViewModel>>(await context.Customers.GetAllCustomerDataAsync());
			return customers;
		}

		// GET: api/customers/5
		[HttpGet("{id}")]
		public async Task<IActionResult> GetCustomerAsync([FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var customer = Mapper.Map<CustomerViewModel>(await context.Customers.GetSingleOrDefaultAsync(c => c.Id == id));

			if (customer == null)
			{
				return NotFound();
			}

			return Ok(customer);
		}

		// PUT: api/customers/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutCustomerAsync([FromRoute] int id, [FromBody] CustomerViewModel customer)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != customer.Id)
			{
				return BadRequest();
			}

			context.Customers.Update(Mapper.Map<Customer>(customer));

			try
			{
				await context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!await CustomerExistsAsync(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/customers
		[HttpPost]
		public async Task<IActionResult> PostCustomerAsync([FromBody] CustomerViewModel customer)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			context.Customers.Add(Mapper.Map<Customer>(customer));
			await context.SaveChangesAsync();

			return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
		}

		// DELETE: api/customers/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCustomerAsync([FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var customer = await context.Customers.GetSingleOrDefaultAsync(m => m.Id == id);
			if (customer == null)
			{
				return NotFound();
			}

			context.Customers.Remove(customer);
			await context.SaveChangesAsync();

			return Ok(customer);
		}

		private async Task<bool> CustomerExistsAsync(int id)
		{
			return await context.Customers.ExistsAsync(e => e.Id == id);
		}
	}
}