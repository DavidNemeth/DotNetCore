using Microsoft.AspNetCore.Identity;
using System;

namespace Blank.Models
{
    public class BlankUser : IdentityUser
    {
        public DateTime FirstTrip { get; set; }
    }
}
