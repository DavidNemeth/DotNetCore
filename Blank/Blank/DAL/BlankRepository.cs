using Blank.DAL.Interfaces;
using Blank.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<Trip> GetAllTrip()
        {
            logger.LogInformation("Getting All Trips from the Database");
            return context.Trips.ToList();
        }
    }
}
