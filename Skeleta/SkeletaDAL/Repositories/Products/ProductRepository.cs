using Microsoft.EntityFrameworkCore;
using SkeletaDAL.ApplicationContext;
using SkeletaDAL.GenericRepository;
using SkeletaDAL.Models;

namespace SkeletaDAL.Repositories.Products
{
	public class ProductRepository : Repository<Product>, IProductRepository
	{
		private ApplicationDbContext appContext
		{
			get { return (ApplicationDbContext)_context; }
		}

		public ProductRepository(DbContext context) : base(context)
		{
		}
	}
}
