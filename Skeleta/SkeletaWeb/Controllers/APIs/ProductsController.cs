using Microsoft.AspNetCore.Mvc;
using SkeletaDAL;
using SkeletaDAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkeletaWeb.Controllers.APIs
{
	[Produces("application/json")]
	[Route("api/Products")]
	public class ProductsController : Controller
	{
		private readonly IUnitOfWork context;

		public ProductsController(IUnitOfWork context)
		{
			this.context = context;
		}

		// GET: api/Products
		[HttpGet]
		public async Task<IEnumerable<Product>> GetProductsAsync()
		{
			return await context.Products.GetAllAsync();
		}

		// GET: api/Products/5
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

		// POST: api/Products
		[HttpPost]
		public async Task<IActionResult> PostProductAsync([FromBody] Product product)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);


			context.Products.Add(product);
			await context.SaveChangesAsync();

			return CreatedAtAction("GetProduct", new { id = product.Id }, product);
		}

		// DELETE: api/Products/5
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

		private async Task<bool> ProductExists(int id) => await context.Products.ExistsAsync(e => e.Id == id);
	}
}