using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkeletaDAL;
using SkeletaDAL.Models;
using SkeletaWeb.Services;
using SkeletaWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkeletaWeb.Controllers
{
	public class CustomersController : Controller
	{
		readonly IUnitOfWork context;
		readonly IServices services;
		public CustomersController(IUnitOfWork context, IServices services)
		{
			this.context = context;
			this.services = services;
		}

		// GET: Customers
		public async Task<IActionResult> IndexAsync()
		{
			var customersVM = Mapper.Map<List<CustomerViewModel>>((await context.Customers.GetAllCustomerDataAsync()));
			return View(customersVM);
		}

		// GET: Customers/Details/5
		public IActionResult Details(int? id)
		{
			if (id == null)
				return NotFound();

			var customerVM = Mapper.Map<CustomerViewModel>(context.Customers.GetSingleOrDefault(c => c.Id == id));

			if (customerVM == null)
				return NotFound();

			return View(customerVM);
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
		public IActionResult Create([Bind("Id,FirstName,LastName,Email")] CustomerViewModel customerVM)
		{
			if (ModelState.IsValid)
			{
				var customer = Mapper.Map<Customer>(customerVM);
				customer.CreatedBy = customerVM.FirstName + customerVM.LastName;
				customer.CreatedDate = DateTime.Now;
				context.Customers.Add(customer);
				context.Complete();

				return RedirectToAction(nameof(IndexAsync));
			}
			return View(customerVM);
		}

		// GET: Customers/Edit/5
		[Authorize]
		public IActionResult Edit(int? id)
		{
			if (id == null)
				return NotFound();

			var customerVM = Mapper.Map<CustomerViewModel>(context.Customers.GetSingleOrDefault(c => c.Id == id));

			if (customerVM == null)
				return NotFound();

			return View(customerVM);
		}

		// POST: Customers/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, [Bind("Id,FirstName,LastName,Email,CreatedDate,CreatedBy")] CustomerViewModel customerVM)
		{
			if (id != customerVM.Id)
				return NotFound();

			var customer = Mapper.Map<Customer>(customerVM);

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
					if (!CustomerExists(customerVM.Id))
						return NotFound();
					else
						throw;
				}
				return RedirectToAction(nameof(IndexAsync));
			}
			return View(customerVM);
		}

		// GET: Customers/Delete/5
		public IActionResult Delete(int? id)
		{
			if (id == null)
				return NotFound();

			var customer = context.Customers.GetSingleOrDefault(c => c.Id == id);
			if (customer == null)
				return NotFound();

			var customerVM = Mapper.Map<CustomerViewModel>(customer);
			return View(customerVM);
		}

		// POST: Customers/Delete/5
		[HttpPost, ActionName(nameof(Delete))]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			var customer = context.Customers.GetSingleOrDefault(m => m.Id == id);
			context.Customers.Remove(customer);
			context.Complete();
			return RedirectToAction(nameof(IndexAsync));
		}

		private bool CustomerExists(int id) => context.Customers.Exists(c => c.Id == id);
	}
}
