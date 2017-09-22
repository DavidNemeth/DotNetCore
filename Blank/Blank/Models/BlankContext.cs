using Microsoft.EntityFrameworkCore;

namespace Blank.Models
{
    public class BlankContext : DbContext
    {
        public BlankContext()
        {

        }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Stop> Stops { get; set; }
    }
}
