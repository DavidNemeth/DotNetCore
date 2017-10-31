using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkeletaDAL.Core;
using SkeletaDAL.Core.CoreModel;
using SkeletaDAL.Core.Interfaces;
using SkeletaDAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static SkeletaDAL.Core.Enums;

namespace SkeletaDAL.ApplicationContext
{
	public interface IDatabaseInitializer
	{
		Task SeedAsync();
	}

	public class DatabaseInitializer : IDatabaseInitializer
	{
		private readonly ApplicationDbContext _context;
		private readonly IAccountManager _accountManager;
		private readonly ILogger _logger;

		public DatabaseInitializer(ApplicationDbContext context, IAccountManager accountManager, ILogger<DatabaseInitializer> logger)
		{
			_accountManager = accountManager;
			_context = context;
			_logger = logger;
		}

		public async Task SeedAsync()
		{
			await _context.Database.MigrateAsync().ConfigureAwait(false);

			if (!await _context.Users.AnyAsync())
			{
				_logger.LogInformation("Generating inbuilt accounts");

				const string adminRoleName = "administrator";
				const string userRoleName = "user";

				await ensureRoleAsync(adminRoleName, "Default administrator", ApplicationPermissions.GetAllPermissionValues());
				await ensureRoleAsync(userRoleName, "Default user", new string[] { });

				await createUserAsync("admin", "tempP@ss123", "Inbuilt Administrator", "admin@capgemini.com", "+36 (20) 000-0000", new string[] { adminRoleName });
				await createUserAsync("user", "tempP@ss123", "Inbuilt Standard User", "user@capgemini.com", "+336 (20) 000-0001", new string[] { userRoleName });

				_logger.LogInformation("Inbuilt account generation completed");
			}

			if (!await _context.Customers.AnyAsync())
			{
				_logger.LogInformation("Seeding initial data");

				var testCustomer1 = new Customer
				{
					FirstName = "David Nemeth",
					LastName = "",
					DateOfBirth = new DateTime(1990, 02, 26),
					Email = "testC1@test.com",
					Gender = Gender.Male,
					CreatedDate = DateTime.Now,
					UpdatedDate = DateTime.Now,
					CreatedBy = "Seeded Test Data",
					UpdatedBy = "No Update since Creation"
				};

				var testCustomer2 = new Customer
				{
					FirstName = "Temp2 Name2",
					LastName = "",
					DateOfBirth = new DateTime(1995, 12, 30),
					Email = "testC2@test.com",
					Gender = Gender.Male,
					CreatedDate = DateTime.Now,
					UpdatedDate = DateTime.Now,
					CreatedBy = "Seeded Test Data",
					UpdatedBy = "No Update since Creation"
				};

				var testCustomer3 = new Customer
				{
					FirstName = "Temp3 Name3",
					LastName = "",
					DateOfBirth = new DateTime(2001, 04, 15),
					Email = "testC3@test.com",
					Gender = Gender.Male,
					CreatedDate = DateTime.Now,
					UpdatedDate = DateTime.Now,
					CreatedBy = "Seeded Test Data",
					UpdatedBy = "No Update since Creation"
				};

				var testCustomer4 = new Customer
				{
					FirstName = "Temp4 Name4",
					LastName = "",
					DateOfBirth = new DateTime(1960, 06, 12),
					Email = "testC4@test.com",
					Gender = Gender.Male,
					CreatedDate = DateTime.Now,
					UpdatedDate = DateTime.Now,
					CreatedBy = "Seeded Test Data",
					UpdatedBy = "No Update since Creation"
				};

				var testProduct1 = new Product
				{
					Name = "Produt1",
					Description = "test description1",
					Code = "tstcd-123",
					Price = 199.9,
					Rating = 4.1,
					UnitsInStock = 32,
					IsActive = true,
					IsDiscontinued = false
				};

				var testProduct2 = new Product
				{
					Name = "Produt2",
					Description = "test description2",
					Code = "tstcd-123",
					Price = 199.9,
					Rating = 4.1,
					UnitsInStock = 32,
					IsActive = true,
					IsDiscontinued = false
				};

				var testProduct3 = new Product
				{
					Name = "Produt3",
					Description = "test description3",
					Code = "tstcd-123",
					Price = 199.9,
					Rating = 4.0,
					UnitsInStock = 32,
					IsActive = true,
					IsDiscontinued = false
				};

				var testProduct4 = new Product
				{
					Name = "Produt4",
					Description = "test description4",
					Code = "tstcd-123",
					Price = 199.9,
					Rating = 2.1,
					UnitsInStock = 32,
					IsActive = true,
					IsDiscontinued = false
				};

				var testOrder1 = new Order
				{
					AppUser = await _context.Users.FirstAsync(),
					Customer = testCustomer1,
					Price = 2500,
					Description = "ugly tshirt from china",
					CreatedDate = DateTime.Now,
					UpdatedDate = DateTime.Now,
					Products = new List<Product>
					{
						testProduct1,
						testProduct2
					}
				};

				var testOrder2 = new Order
				{
					AppUser = await _context.Users.FirstAsync(),
					Customer = testCustomer1,
					Price = 2500,
					Description = "Big ass TeLieVision",
					CreatedDate = DateTime.Now,
					UpdatedDate = DateTime.Now,
					Products = new List<Product>
					{
						testProduct3,
						testProduct4
					}
				};

				_context.Customers.Add(testCustomer1);
				_context.Customers.Add(testCustomer2);
				_context.Customers.Add(testCustomer3);
				_context.Customers.Add(testCustomer4);

				_context.Orders.Add(testOrder1);
				_context.Orders.Add(testOrder2);

				_context.Products.Add(testProduct1);
				_context.Products.Add(testProduct2);
				_context.Products.Add(testProduct3);
				_context.Products.Add(testProduct4);


				await _context.SaveChangesAsync();

				_logger.LogInformation("Seeding initial data completed");
			}
		}

		private async Task ensureRoleAsync(string roleName, string description, string[] claims)
		{
			if ((await _accountManager.GetRoleByNameAsync(roleName)) == null)
			{
				var applicationRole = new ApplicationRole(roleName, description);

				var result = await this._accountManager.CreateRoleAsync(applicationRole, claims);

				if (!result.Item1)
					throw new Exception($"Seeding \"{description}\" role failed. Errors: {string.Join(Environment.NewLine, result.Item2)}");
			}
		}

		private async Task<ApplicationUser> createUserAsync(string userName, string password, string fullName, string email, string phoneNumber, string[] roles)
		{
			var applicationUser = new ApplicationUser
			{
				UserName = userName,
				FullName = fullName,
				Email = email,
				PhoneNumber = phoneNumber,
				EmailConfirmed = true,
				IsEnabled = true
			};

			var result = await _accountManager.CreateUserAsync(applicationUser, roles, password);

			if (!result.Item1)
				throw new Exception($"Seeding \"{userName}\" user failed. Errors: {string.Join(Environment.NewLine, result.Item2)}");

			return applicationUser;
		}
	}
}