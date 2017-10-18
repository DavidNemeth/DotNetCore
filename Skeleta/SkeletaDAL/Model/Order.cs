using SkeletaDAL.Core;
using SkeletaDAL.Core.CoreModel;
using SkeletaDAL.Model.Interfaces;
using System;

namespace SkeletaDAL.Model
{
	public class Order : AuditableEntity
	{
		public int Id { get; set; }
		public decimal Discount { get; set; }
		public string Comments { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateModified { get; set; }


		public string AppUserId { get; set; }
		public ApplicationUser AppUser { get; set; }

		public int CustomerId { get; set; }
		public Customer Customer { get; set; }
	}
}
