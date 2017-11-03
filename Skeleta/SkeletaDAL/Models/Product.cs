using SkeletaDAL.Models.Interfaces;

namespace SkeletaDAL.Models
{
	public class Product : AuditableEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public string Description { get; set; }
		public double Price { get; set; }
		public double Rating { get; set; }
		public string ImageUrl { get; set; }
		public int UnitsInStock { get; set; }
		public bool IsActive { get; set; }
		public bool IsDiscontinued { get; set; }
	}
}
