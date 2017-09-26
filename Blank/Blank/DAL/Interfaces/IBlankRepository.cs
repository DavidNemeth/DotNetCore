using Blank.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blank.DAL.Interfaces
{
    public interface IBlankRepository
    {
        IEnumerable<Trip> GetAllTrip();
        void AddTrip(Trip newTrip);
        Trip GetTripByname(string tripName);

        void AddStop(string tripName, Stop newStop);

        Task<bool> SaveChangesAsync();
    }
}