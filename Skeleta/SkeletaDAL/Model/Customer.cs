using SkeletaDAL.Model.Interfaces;
using System.Collections.Generic;

namespace SkeletaDAL.Model
{
	public class Customer : AuditableEntity
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }

		public IEnumerable<Order> Orders { get; set; }
		public string Description { get; set; }
	}
}
