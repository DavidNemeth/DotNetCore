using Microsoft.EntityFrameworkCore;
using SkeletaDAL.ApplicationContext;
using SkeletaDAL.Core.CoreModel;
using SkeletaDAL.GenericRepository;
using SkeletaDAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkeletaDAL.Repositories.Customers
{
	public class CustomerRepository : Repository<Customer>, ICustomerRepository
	{
		private ApplicationDbContext appContext
		{
			get { return (ApplicationDbContext)_context; }
		}
		public CustomerRepository(DbContext context) : base(context)
		{
		}

		public async Task<List<Customer>> GetAllCustomerDataAsync()
		{
			return await appContext.Customers
				.OrderBy(c => c.LastName)
				.Include(c => c.Orders).ThenInclude(c => c.AppUser)
				.ToListAsync();
		}

		public async Task<PagingResult<Customer>> GetCustomersPageAsync(int skip, int take)
		{
			var totalRecords = await appContext.Customers.CountAsync();
			var customers = await appContext.Customers
				.OrderBy(c => c.LastName)
				.Include(c => c.Orders).ThenInclude(c => c.AppUser)
				.Skip(skip)
				.Take(take)
				.ToListAsync();

			return new PagingResult<Customer>(customers, totalRecords);
		}
	}
}
