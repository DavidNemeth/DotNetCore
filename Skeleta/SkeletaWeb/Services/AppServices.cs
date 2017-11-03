using SkeletaDAL;
using System.Threading.Tasks;

namespace SkeletaWeb.Services
{
	public class AppServices : IServices
	{
		private IUnitOfWork context;

		public AppServices(IUnitOfWork context)
		{
			this.context = context;
		}

		public async Task<int> GetAverageCustomerAge()
		{
			var customers = await context.Customers.GetAllAsync();
			var totalAge = 0;
			var totalCustomers = await context.Customers.CountAsync();

			foreach (var customer in customers)
			{
				totalAge += customer.DateOfBirth.GetAge();
			}

			return totalAge / totalCustomers;
		}
	}
}