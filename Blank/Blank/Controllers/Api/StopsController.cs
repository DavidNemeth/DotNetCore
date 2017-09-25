using AutoMapper;
using Blank.DAL.Interfaces;
using Blank.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Blank.Controllers.Api
{
    [Route("/api/trips/{tripName}/stops")]
    public class StopsController : Controller
    {
        private IBlankRepository repo;
        private ILogger<StopsController> logger;

        public StopsController(IBlankRepository repo, ILogger<StopsController> logger)
        {
            this.repo = repo;
            this.logger = logger;
        }

        [HttpGet("")]
        public IActionResult Get(string tripName)
        {
            try
            {
                var trip = repo.GetTripByname(tripName);
                var stops = Mapper.Map<IEquatable<StopViewModel>>(trip.Stops.OrderBy(t => t.Order).ToList());

                return Ok(stops);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get stop: {ex}");
            }

            return BadRequest("Failed to get Stops");
        }
    }
}
