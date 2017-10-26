namespace SkeletaWeb.ViewModels
{
	public class OrderViewModel
	{
		public int Id { get; set; }
		public decimal Price { get; set; }
		public string Description { get; set; }

		public UserViewModel User { get; set; }
	}
}