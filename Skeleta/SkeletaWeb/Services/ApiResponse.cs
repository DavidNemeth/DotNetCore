using Microsoft.AspNetCore.Mvc.ModelBinding;
using SkeletaWeb.ViewModels;

namespace SkeletaWeb.Services
{
	public class ApiResponse
	{
		public bool Status { get; set; }
		public CustomerViewModel Customer { get; set; }
		public ModelStateDictionary ModelState { get; set; }
	}
}