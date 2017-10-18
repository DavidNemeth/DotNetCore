using Microsoft.EntityFrameworkCore;
using SkeletaDAL.ApplicationContext;
using SkeletaDAL.GenericRepository;
using SkeletaDAL.Model;

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


	}
}
