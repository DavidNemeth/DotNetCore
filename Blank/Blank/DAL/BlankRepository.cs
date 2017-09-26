using Blank.DAL.Interfaces;
using Blank.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blank.DAL
{
    public class BlankRepository : IBlankRepository
    {
        private ILogger<BlankRepository> logger;
        private BlankContext context;

        public BlankRepository(BlankContext context, ILogger<BlankRepository> logger)
        {
            this.logger = logger;
            this.context = context;
        }

        public void AddStop(string tripName, Stop newStop)
        {
            var trip = GetTripByname(tripName);
            if (trip != null)
            {
                trip.Stops.Add(newStop);
                context.Stops.Add(newStop);
            }
        }

        public void AddTrip(Trip newTrip)
        {
            context.Add(newTrip);
        }

        public IEnumerable<Trip> GetAllTrip()
        {
            logger.LogInformation("Getting All Trips from the Database");
            return context.Trips.ToList();
        }

        public Trip GetTripByname(string tripName)
        {
            return context.Trips
                .Include(t => t.Stops)
                .Where(t => t.Name == tripName
                ).FirstOrDefault();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await context.SaveChangesAsync()) > 0;
        }
    }
}
