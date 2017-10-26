using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SkeletaDAL.Core.CoreModel;
using SkeletaDAL.Models;
using SkeletaDAL.Models.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SkeletaDAL.ApplicationContext
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
	{
		public string CurrentUserId { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Order> Orders { get; set; }

		public ApplicationDbContext(DbContextOptions options) : base(options)
		{ }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<ApplicationUser>().HasMany(u => u.Claims).WithOne().HasForeignKey(c => c.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
			builder.Entity<ApplicationUser>().HasMany(u => u.Roles).WithOne().HasForeignKey(r => r.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);

			builder.Entity<ApplicationRole>().HasMany(r => r.Claims).WithOne().HasForeignKey(c => c.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);
			builder.Entity<ApplicationRole>().HasMany(r => r.Users).WithOne().HasForeignKey(r => r.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Customer>().Property(c => c.FirstName).IsRequired().HasMaxLength(100);
			builder.Entity<Customer>().HasIndex(c => c.FirstName);
			builder.Entity<Customer>().Property(c => c.Email).HasMaxLength(100);
			builder.Entity<Customer>().ToTable($"App{nameof(this.Customers)}");

			builder.Entity<Order>().Property(o => o.Description).HasMaxLength(500);
			builder.Entity<Order>().ToTable($"App{nameof(this.Orders)}");
		}

		public override int SaveChanges()
		{
			UpdateAuditEntities();
			return base.SaveChanges();
		}

		public override int SaveChanges(bool acceptAllChangesOnSuccess)
		{
			UpdateAuditEntities();
			return base.SaveChanges(acceptAllChangesOnSuccess);
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			UpdateAuditEntities();
			return base.SaveChangesAsync(cancellationToken);
		}

		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
		{
			UpdateAuditEntities();
			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}

		private void UpdateAuditEntities()
		{
			var modifiedEntries = ChangeTracker.Entries()
				.Where(x => x.Entity is IAuditableEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

			foreach (var entry in modifiedEntries)
			{
				var entity = (IAuditableEntity)entry.Entity;
				var now = DateTime.UtcNow;

				if (entry.State == EntityState.Added)
				{
					entity.CreatedDate = now;
					entity.CreatedBy = CurrentUserId;
				}
				else
				{
					base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
					base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
				}

				entity.UpdatedDate = now;
				entity.UpdatedBy = CurrentUserId;
			}
		}
	}
}