using SkeletaDAL.Core.CoreModel;
using SkeletaDAL.GenericRepository;
using SkeletaDAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkeletaDAL.Repositories.Customers
{
	public interface ICustomerRepository : IRepository<Customer>
	{
		Task<List<Customer>> GetAllCustomerDataAsync();

		Task<PagingResult<Customer>> GetCustomersPageAsync(int skip, int take);
	}
}