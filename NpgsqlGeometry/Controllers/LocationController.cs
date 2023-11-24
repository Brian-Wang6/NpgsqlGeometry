using Microsoft.AspNetCore.Mvc;
using NpgsqlGeometry.Model;
using NpgsqlGeometry.Serivce.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NpgsqlGeometry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }


        // GET: api/<LocationController>
        [HttpGet]
        public async Task<IActionResult> GetLocationsAsync()
        {
            var locations = await _locationService.GetLocationsAsync();
            return Ok(locations);
        }

        // GET api/<LocationController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocationByIdAsync(long id)
        {
            var location = await _locationService.GetLocationByIDAsync(id);
            return Ok(location);
        }

        // POST api/<LocationController>
        [HttpPost]
        public async Task<IActionResult> SaveLocation(Location location)
        {
            await _locationService.SaveLocationAsync(location);
            return Ok();
        }

        // PUT api/<LocationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LocationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
