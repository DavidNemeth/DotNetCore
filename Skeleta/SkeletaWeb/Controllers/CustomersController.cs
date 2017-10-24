using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkeletaDAL;
using SkeletaDAL.Models;
using SkeletaWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkeletaWeb.Controllers
{
	public class CustomersController : Controller
	{
		private readonly IUnitOfWork context;

		public CustomersController(IUnitOfWork context)
		{
			this.context = context;
		}

		// GET: Customers
		[Authorize]
		public async Task<IActionResult> Index()
		{
			var model = await context.Customers.GetAllCustomerDataAsync();
			var customers = Mapper.Map<List<CustomerViewModel>>(model);

			return View(customers);
		}

		// GET: Customers/Details/5
		public IActionResult Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var model = context.Customers.GetSingleOrDefault(c => c.Id == id);
			var customer = Mapper.Map<CustomerViewModel>(model);

			if (customer == null)
			{
				return NotFound();
			}

			return View(customer);
		}

		// GET: Customers/Create
		[Authorize]
		public IActionResult Create()
		{
			return View();
		}

		// POST: Customers/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create([Bind("Id,FirstName,LastName,Email")] CustomerViewModel model)
		{
			if (ModelState.IsValid)
			{
				var customer = Mapper.Map<Customer>(model);
				customer.CreatedBy = model.FirstName + model.LastName;
				customer.CreatedDate = DateTime.Now;
				context.Customers.Add(customer);
				context.Complete();

				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}

		// GET: Customers/Edit/5
		public IActionResult Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var model = context.Customers.GetSingleOrDefault(c => c.Id == id);
			var customer = Mapper.Map<CustomerViewModel>(model);


			if (customer == null)
			{
				return NotFound();
			}
			return View(customer);
		}

		// POST: Customers/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, [Bind("Id,FirstName,LastName,Email,CreatedDate,CreatedBy")] CustomerViewModel model)
		{
			if (id != model.Id)
			{
				return NotFound();
			}

			var customer = Mapper.Map<Customer>(model);

			if (ModelState.IsValid)
			{
				try
				{
					customer.UpdatedBy = customer.FirstName + customer.LastName;
					customer.UpdatedDate = DateTime.Now;

					context.Customers.Update(customer);
					context.Complete();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!CustomerExists(model.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(customer);
		}

		// GET: Customers/Delete/5
		public IActionResult Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var model = context.Customers.GetSingleOrDefault(c => c.Id == id);
			if (model == null)
			{
				return NotFound();
			}
			var customer = Mapper.Map<CustomerViewModel>(model);

			return View(customer);
		}

		// POST: Customers/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			var customer = context.Customers.GetSingleOrDefault(m => m.Id == id);
			context.Customers.Remove(customer);
			context.Complete();
			return RedirectToAction(nameof(Index));
		}

		private bool CustomerExists(int id)
		{
			return context.Customers.Exists(c => c.Id == id);
		}
	}
}
