using System;
using System.ComponentModel.DataAnnotations;

namespace SkeletaWeb.ViewModels
{
	public class ViewModelBase
	{
		[MaxLength(256)]
		public string CreatedBy { get; set; }
		[MaxLength(256)]
		public string UpdatedBy { get; set; }
		public DateTime UpdatedDate { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}
