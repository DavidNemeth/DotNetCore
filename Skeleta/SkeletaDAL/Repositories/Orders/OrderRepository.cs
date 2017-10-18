using Microsoft.EntityFrameworkCore;
using SkeletaDAL.ApplicationContext;
using SkeletaDAL.GenericRepository;
using SkeletaDAL.Model;

namespace SkeletaDAL.Repositories.Orders
{
	public class OrderRepository : Repository<Order>, IOrderRepository
	{
		private ApplicationDbContext appContext
		{
			get { return (ApplicationDbContext)_context; }
		}
		public OrderRepository(DbContext context) : base(context)
		{
		}


	}
}
