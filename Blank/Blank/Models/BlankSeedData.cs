using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blank.Models
{
    public class BlankSeedData
    {
        private BlankContext context;
        private UserManager<BlankUser> userManager;

        public BlankSeedData(BlankContext context, UserManager<BlankUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task EnsureSeedData()
        {
            if (await userManager.FindByEmailAsync("davidnemeth@mail.com") == null)
            {
                var user = new BlankUser()
                {
                    UserName = "davidnemeth",
                    Email = "davidnemeth@mail.com"
                };

                await userManager.CreateAsync(user, "P@ssw0rd!");
            }

            if (!context.Trips.Any())
            {
                var usTrips = new Trip()
                {
                    DateCreated = DateTime.UtcNow,
                    Name = "US Trip",
                    UserName = "davidnemeth",
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
                    UserName = "davidnemeth",
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
