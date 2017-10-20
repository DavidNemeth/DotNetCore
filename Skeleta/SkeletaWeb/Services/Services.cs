using Microsoft.Extensions.Logging;
using SkeletaDAL;
using System;

namespace SkeletaWeb.Services
{
	public class Services : IServices
	{
		private IUnitOfWork context;
		private ILogger logger;

		public Services(IUnitOfWork context, ILogger logger)
		{
			this.context = context;
			this.logger = logger;
		}

		public int GetAverageCustomerAge()
		{
			try
			{
				var customers = context.Customers.GetAll();
				var totalAge = 0;
				var totalCustomers = context.Customers.Count();

				foreach (var customer in customers)
				{
					totalAge += customer.DateOfBirth.GetAge();
				}

				return totalAge / totalCustomers;
			}
			catch (Exception ex)
			{
				logger.LogError($"Failed to get Average Customer Age, with error message: {ex}");
				return -1;
			}
		}
	}
}
