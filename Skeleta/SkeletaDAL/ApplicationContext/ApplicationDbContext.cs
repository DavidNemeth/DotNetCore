using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SkeletaDAL.Model;

namespace SkeletaDAL.ApplicationContext
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
	{
		public string CurrentUserId { get; set; }
		public DbSet<Customer> Staffs { get; set; }
		public DbSet<Order> Students { get; set; }
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{ }
	}
}