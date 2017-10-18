using System;

namespace SkeletaDAL.Model
{
	public class BaseEntity
	{
		public int Id { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ModifiedDate { get; set; }
		public string Note { get; set; }
	}
}
