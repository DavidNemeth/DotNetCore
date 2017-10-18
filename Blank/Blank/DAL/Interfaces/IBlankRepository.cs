using Blank.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blank.DAL.Interfaces
{
    public interface IBlankRepository
    {
        IEnumerable<Trip> GetAllTrips();
        Trip GetTripByName(string tripName, string username);

        void AddTrip(Trip trip);
        void AddStop(string tripName, string username, Stop newStop);

        Task<bool> SaveChangesAsync();
        List<Trip> GetTripsByWhatever<T>(T name);

    }
}