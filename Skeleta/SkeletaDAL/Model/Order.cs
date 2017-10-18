using System.ComponentModel.DataAnnotations;

namespace SkeletaDAL.Model
{
	public class Order : BaseEntity
	{
		[Required]
		public string Name { get; set; }
		public string Description { get; set; }
		[Required]
		public string ShippingAdress { get; set; }
		public string TrackingNumber { get; set; }

		public Customer Customer { get; set; }
	}
}
