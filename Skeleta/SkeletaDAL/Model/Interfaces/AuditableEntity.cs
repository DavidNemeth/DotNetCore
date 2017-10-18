using System;
using System.ComponentModel.DataAnnotations;

namespace SkeletaDAL.Model.Interfaces
{
	/*Use this class to Log entity Changes*/
	public class AuditableEntity
	{
		[MaxLength(256)]
		public string CreatedBy { get; set; }
		[MaxLength(256)]
		public string UpdatedBy { get; set; }
		public DateTime UpdatedDate { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}
