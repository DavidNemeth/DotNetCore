using SkeletaDAL.Core.CoreModel;
using SkeletaDAL.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace SkeletaDAL.Models
{
	public class Order : AuditableEntity
	{
		public int Id { get; set; }
		public decimal Price { get; set; }
		public string Description { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateModified { get; set; }

		public string AppUserId { get; set; }
		public ApplicationUser AppUser { get; set; }

		public int CustomerId { get; set; }
		public Customer Customer { get; set; }

		public IEnumerable<Product> Products { get; set; }
	}
}