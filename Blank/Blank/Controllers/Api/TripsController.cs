using AutoMapper;
using Blank.DAL.Interfaces;
using Blank.Models;
using Blank.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blank.Controllers.Api
{
    [Route("api/trips")]
    [Authorize]
    public class TripsController : Controller
    {
        private ILogger<TripsController> logger;
        private IBlankRepository repo;

        public TripsController(IBlankRepository repo, ILogger<TripsController> logger)
        {
            this.logger = logger;
            this.repo = repo;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var results = repo.GetTripsByUsername(User.Identity.Name);

                return Ok(Mapper.Map<IEnumerable<TripViewModel>>(results));
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get All Trips: {ex}");

                return BadRequest("Error occurred");
            }

        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]TripViewModel theTrip)
        {
            if (ModelState.IsValid)
            {
                // Save to the Database
                var newTrip = Mapper.Map<Trip>(theTrip);

                newTrip.UserName = User.Identity.Name;

                repo.AddTrip(newTrip);

                if (await repo.SaveChangesAsync())
                {
                    return Created($"api/trips/{theTrip.Name}", Mapper.Map<TripViewModel>(newTrip));
                }
            }

            return BadRequest("Failed to save the trip");
        }
    }
}