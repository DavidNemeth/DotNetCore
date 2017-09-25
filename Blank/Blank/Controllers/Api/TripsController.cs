using AutoMapper;
using Blank.DAL.Interfaces;
using Blank.Models;
using Blank.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blank.Controllers.Api
{
    [Route("api/trips")]
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
                var results = repo.GetAllTrip();
                var tripList = Mapper.Map<IEnumerable<TripViewModel>>(results);

                return Ok(tripList);
            }
            catch (Exception ex)
            {
                // TODO Logging error
                logger.LogError($"Failed to get all Trips {ex}");
                return BadRequest("Error occured");
            }

        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]TripViewModel trip)
        {
            if (ModelState.IsValid)
            {
                // save to db
                //var newTrip = new Trip()
                //{
                //    Name = trip.Name,
                //    DateCreated = trip.Created
                //};

                var newTrip = Mapper.Map<Trip>(trip);
                repo.AddTrip(newTrip);

                if (await repo.SaveChangesAsync())
                    return Created($"api/trips/{trip.Name}", Mapper.Map<TripViewModel>(newTrip));
            }
            return BadRequest("Failed to save the Trip");
        }
    }
}
