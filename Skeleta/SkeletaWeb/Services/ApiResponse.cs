namespace SkeletaWeb.Services
{
	public class ApiResponse
	{
		public bool Status { get; set; }
		public Customer Customer { get; set; }
		public ModelStateDictionary ModelState { get; set; }
	}
}
