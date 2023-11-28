using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using NpgsqlGeometry.Model;
using NpgsqlGeometry.Response;
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
            var polygonResponse = GeomToPoint(locations);
            return Ok(polygonResponse);
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
        public async Task<IActionResult> SaveLocation(Response.Polygon polygon)
        {
            await _locationService.SaveLocationAsync(polygon);
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

        private PolygonResponse GeomToPoint(List<Model.Location> locations)
        {
            var polygonResponse = new PolygonResponse();
            foreach (var location in locations)
            {
                Response.Polygon polygon = new Response.Polygon();
                polygon.LocationUID = location.LocationUID;
                Geometry? geometry = location.RegionCoords;
                var coordinates = geometry.Coordinates;
                var pLists = geometry.ToText().Replace("POLYGON ((", "").Replace("))", "").Split("), (");
                foreach (var pList in pLists)
                {
                    List<Response.Point> ring = new List<Response.Point>();
                    var points = pList.Split(", ");
                    foreach (var point in points)
                    {
                        double x_coordinate = double.Parse(point.Split(" ")[0]);
                        double y_coordinate = double.Parse(point.Split(" ")[1]);
                        ring.Add(new Response.Point(x_coordinate, y_coordinate));
                    }
                    polygon.Points.Add(ring);
                }
                polygonResponse.Polygons.Add(polygon);
            }
            return polygonResponse;
        }
    }
}
