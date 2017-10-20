using SkeletaDAL.Models.Interfaces;
using System;
using System.Collections.Generic;
using static SkeletaDAL.Core.Enums;

namespace SkeletaDAL.Models
{
	public class Customer : AuditableEntity
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string Email { get; set; }
		public Gender Gender { get; set; }

		public IEnumerable<Order> Orders { get; set; }
	}
}
