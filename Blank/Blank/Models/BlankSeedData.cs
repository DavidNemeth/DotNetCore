using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blank.Models
{
    public class BlankSeedData
    {
        private BlankContext context;

        public BlankSeedData(BlankContext context)
        {
            this.context = context;
        }

        public async Task EnsureSeedData()
        {
            if (!context.Trips.Any())
            {
                var usTrips = new Trip()
                {
                    DateCreated = DateTime.UtcNow,
                    Name = "US Trip",
                    UserName = "TODO",
                    Stops = new List<Stop>()
                    {

                    }
                };
                context.Trips.Add(usTrips);

                context.Stops.AddRange(usTrips.Stops);

                var worldTrip = new Trip()
                {
                    DateCreated = DateTime.UtcNow,
                    Name = "WroldTrip Trip",
                    UserName = "TODO",
                    Stops = new List<Stop>()
                    {


                    }
                };
                context.Trips.Add(worldTrip);

                context.Stops.AddRange(worldTrip.Stops);

                await context.SaveChangesAsync();
            }
        }
    }
}
