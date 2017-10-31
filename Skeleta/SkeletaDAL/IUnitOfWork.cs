using SkeletaDAL.Repositories.Customers;
using SkeletaDAL.Repositories.Orders;
using SkeletaDAL.Repositories.Products;

namespace SkeletaDAL
{
	public interface IUnitOfWork
	{
		ICustomerRepository Customers { get; }
		IOrderRepository Orders { get; }
		IProductRepository Products { get; }

		int Complete();
	}
}