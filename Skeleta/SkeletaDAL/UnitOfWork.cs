﻿using SkeletaDAL.ApplicationContext;
using SkeletaDAL.Repositories.Customers;
using SkeletaDAL.Repositories.Orders;
using SkeletaDAL.Repositories.Products;
using System.Threading.Tasks;

namespace SkeletaDAL
{
	public class UnitOfWork : IUnitOfWork
	{
		private ApplicationDbContext _context;

		private IProductRepository _products;
		private ICustomerRepository _customers;
		private IOrderRepository _orders;

		public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;
		}

		public IProductRepository Products
		{
			get
			{
				if (_products == null)
					_products = new ProductRepository(_context);

				return _products;
			}
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

		public async Task<int> SaveChangesAsync()
		{
			return await _context.SaveChangesAsync();
		}
	}
}