using SkeletaDAL.Repositories.Customers;
using SkeletaDAL.Repositories.Orders;

namespace SkeletaDAL
{
	public interface IUnitOfWork
	{
		ICustomerRepository Customers { get; }
		IOrderRepository Orders { get; }

		int Complete();
	}
}