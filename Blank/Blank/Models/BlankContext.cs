using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Blank.Models
{
    public class BlankContext : IdentityDbContext<BlankUser>
    {
        private IConfigurationRoot config;

        public BlankContext(IConfigurationRoot config, DbContextOptions options)
            : base(options)
        {
            this.config = config;
        }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Stop> Stops { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(config["ConnectionStrings:BlankAppContextConnection"]);
        }
    }
}
