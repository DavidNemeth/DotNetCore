using SkeletaDAL.ApplicationContext;
using SkeletaDAL.Repositories.Customers;
using SkeletaDAL.Repositories.Orders;

namespace SkeletaDAL
{
	public class UnitOfWork : IUnitOfWork
	{
		private ApplicationDbContext _context;

		private ICustomerRepository _customers;
		private IOrderRepository _orders;

		public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;
		}


		public ICustomerRepository Customers
		{
			get
			{
				if (_customers == null)
					_customers = new CustomerRepository(_context);

				return _customers;
			}
		}
		public IOrderRepository Orders
		{
			get
			{
				if (_orders == null)
					_orders = new OrderRepository(_context);

				return _orders;
			}
		}


		public int Complete()
		{
			return _context.SaveChanges();
		}
	}
}
