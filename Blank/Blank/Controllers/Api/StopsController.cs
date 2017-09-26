using AutoMapper;
using Blank.DAL.Interfaces;
using Blank.Models;
using Blank.Services;
using Blank.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Blank.Controllers.Api
{
    [Route("/api/trips/{tripName}/stops")]
    public class StopsController : Controller
    {
        private GeoCoordsService coordsService;
        private IBlankRepository repo;
        private ILogger<StopsController> logger;

        public StopsController(IBlankRepository repo, ILogger<StopsController> logger, GeoCoordsService coordsService)
        {
            this.repo = repo;
            this.logger = logger;
            this.coordsService = coordsService;
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
        [HttpPost("")]
        public async Task<IActionResult> Set(string tripName, [FromBody]StopViewModel stopVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newStop = Mapper.Map<Stop>(stopVm);
                    var result = await coordsService.GetCoordsAsync(newStop.Name);

                    if (!result.Success)
                        logger.LogError(result.Message);
                    else
                    {
                        newStop.Latitude = result.Latitude;
                        newStop.Longitude = result.Longitude;
                    }
                    repo.AddStop(tripName, newStop);

                    if (await repo.SaveChangesAsync())
                    {
                        return Created($"/api/trips/{tripName}/stops/{newStop.Name}", Mapper.Map<StopViewModel>(newStop));
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to save new Stop: {ex}");
            }
            return BadRequest("Failed to add Stop to Trip");
        }
    }
}
