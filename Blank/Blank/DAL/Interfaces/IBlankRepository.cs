using Blank.Models;
using System.Collections.Generic;

namespace Blank.DAL.Interfaces
{
    public interface IBlankRepository
    {
        IEnumerable<Trip> GetAllTrip();
    }
}