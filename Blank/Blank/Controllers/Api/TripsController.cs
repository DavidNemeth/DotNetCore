using Blank.DAL.Interfaces;
using Blank.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Blank.Controllers.Api
{
    [Route("api/trips")]
    public class TripsController : Controller
    {
        private IBlankRepository repo;

        public TripsController(IBlankRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok(repo.GetAllTrip());
        }

        [HttpPost("")]
        public IActionResult Post([FromBody]TripViewModel trip)
        {
            if (ModelState.IsValid)
            {
                return Created($"api/trips/{trip.Name}", trip);
            }
            return BadRequest("Invalid Data");
        }
    }
}
