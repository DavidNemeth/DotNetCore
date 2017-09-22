using Blank.DAL.Interfaces;
using Blank.Models;
using System.Collections.Generic;
using System.Linq;

namespace Blank.DAL
{
    public class BlankRepository : IBlankRepository
    {
        private BlankContext context;

        public BlankRepository(BlankContext context)
        {
            this.context = context;
        }

        public IEnumerable<Trip> GetAllTrip()
        {
            return context.Trips.ToList();
        }
    }
}
