using SkeletaDAL;

namespace SkeletaWeb.Services
{
	public class AppServices : IServices
	{
		private IRepository context;

		public AppServices(IRepository context)
		{
			this.context = context;
		}

		public int GetAverageCustomerAge()
		{
			var customers = context.Customers.GetAllAsync();
			var totalAge = 0;
			var totalCustomers = context.Customers.CountAsync();

			foreach (var customer in customers)
			{
				totalAge += customer.DateOfBirth.GetAge();
			}

			return totalAge / totalCustomers;
		}
	}
}