using Blank.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blank.DAL.Interfaces
{
    public interface IBlankRepository
    {
        IEnumerable<Trip> GetAllTrip();
        IEnumerable<Trip> GetTripsByUserName(string username);
        Trip GetTripByname(string tripName);
        Trip GetUserTripByName(string tripName, string username);


        void AddTrip(Trip newTrip);
        void AddStop(string tripName, Stop newStop, string username);

        Task<bool> SaveChangesAsync();
    }
}