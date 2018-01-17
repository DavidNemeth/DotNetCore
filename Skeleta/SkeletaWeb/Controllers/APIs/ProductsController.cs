using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkeletaDAL;
using SkeletaDAL.Models;
using SkeletaWeb.Services;
using SkeletaWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkeletaWeb.Controllers.APIs
{
	[Produces("application/json")]
	[Route("api/products")]
	public class ProductsController : BaseController
	{
		public ProductsController(IUnitOfWork context, IServices services) : base(context, services)
		{
		}


		// GET: api/products
		[HttpGet]
		public async Task<IEnumerable<ProductViewModel>> GetProductsAsync()
		{
			var products = Mapper.Map<List<ProductViewModel>>(await context.Products.GetAllAsync());
			return products;
		}

		// GET: api/products/5
		[HttpGet("{id}")]
		public async Task<IActionResult> GetProductAsync([FromRoute] int id)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var product = await context.Products.GetSingleOrDefaultAsync(m => m.Id == id);

			if (product == null)
				return NotFound();

			return Ok(product);
		}

		// POST: api/products
		[HttpPost]
		public async Task<IActionResult> PostProductAsync([FromBody] ProductViewModel product)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			var itemToAdd = Mapper.Map<Product>(product);
			itemToAdd.CreatedDate = DateTime.Now;
			context.Products.Add(itemToAdd);
			await context.SaveChangesAsync();

			return CreatedAtAction("GetProduct", new { id = product.Id }, product);
		}
		// PUT: api/products/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutProductAsync([FromRoute] int id, [FromBody] ProductViewModel product)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != product.Id)
			{
				return BadRequest();
			}

			context.Products.Update(Mapper.Map<Product>(product));

			try
			{
				await context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!await ProductExistsAsync(id))
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

		// DELETE: api/products/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProductAsync([FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var product = await context.Products.GetSingleOrDefaultAsync(m => m.Id == id);
			if (product == null)
				return NotFound();

			context.Products.Remove(product);
			await context.SaveChangesAsync();

			return Ok(product);
		}

		private async Task<bool> ProductExistsAsync(int id) => await context.Products.ExistsAsync(e => e.Id == id);
	}
}