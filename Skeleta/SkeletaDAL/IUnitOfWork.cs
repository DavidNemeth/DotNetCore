using SkeletaDAL.Repositories.Customers;
using SkeletaDAL.Repositories.Orders;
using SkeletaDAL.Repositories.Products;
using System.Threading.Tasks;

namespace SkeletaDAL
{
	public interface IUnitOfWork
	{
		ICustomerRepository Customers { get; }
		IOrderRepository Orders { get; }
		IProductRepository Products { get; }

		Task<int> SaveChangesAsync();
	}
}