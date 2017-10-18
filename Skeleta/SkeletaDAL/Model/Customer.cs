using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SkeletaDAL.Model
{
	public class Customer : BaseEntity
	{
		[Required]
		public string FirstName { get; set; }
		public string LastName { get; set; }

		[EmailAddress]
		public string Email { get; set; }

		public IEnumerable<Order> Orders { get; set; }
	}
}
