using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;
using NpgsqlGeometry.DBContext;
using NpgsqlGeometry.Model;
using NpgsqlGeometry.Serivce.Interface;
using System.Text;

namespace NpgsqlGeometry.Serivce
{
    public class LocationService : ILocationService
    {
        private readonly PostgreDBContext postgresDBContext;

        public LocationService(PostgreDBContext postgresDBContext)
        {
            this.postgresDBContext = postgresDBContext;
        }

        public async Task<List<Model.Location>> GetLocationsAsync()
        {
            var locations = await postgresDBContext.Locations.ToListAsync();

            return locations;
        }

        public async Task<Model.Location> GetLocationByIDAsync(long id)
        {
            var location = await postgresDBContext.Locations.FirstOrDefaultAsync(l => l.LocationUID == id);
            return location ?? new Model.Location();
        }

        public async Task DeleteLocationAsync(long id)
        {
            var dbLocation = await GetLocationByIDAsync(id);
            if (dbLocation.RegionCoords == null)
            {
                return;
            }
            postgresDBContext.Locations.Remove(dbLocation);
            var res = await postgresDBContext.SaveChangesAsync();
        }

        public async Task SaveLocationAsync(Response.Polygon polygon)
        {
            Model.Location location = ConvertPolygonToLocation(polygon);
            var dbLocation = await GetLocationByIDAsync(polygon.LocationUID);
            int res;
            if (dbLocation.RegionCoords != null)
            {
                dbLocation.RegionCoords = location.RegionCoords;
                postgresDBContext.Locations.Update(dbLocation);                
            }
            else
            {
                postgresDBContext.Locations.Add(location);                
            }
            res = await postgresDBContext.SaveChangesAsync();
        }

        private Model.Location ConvertPolygonToLocation(Response.Polygon polygon)
        {
            Model.Location location = new Model.Location();
            location.LocationUID = polygon.LocationUID;
            
            string wkt = ConvertPolygonToWellKnownText(polygon);

            WKTReader wktReader = new WKTReader();
            Geometry coords = wktReader.Read(wkt);
            location.RegionCoords = coords;

            return location;
        }

        private string ConvertPolygonToWellKnownText(Response.Polygon polygon)
        {
            StringBuilder wktBuilder = new StringBuilder();
            wktBuilder.Append("POLYGON(");
            var points = polygon.Points;
            for (var r = 0; r < points.Count; r++)
            {
                wktBuilder.Append("(");
                for (var i = 0; i < points[r].Count; i++)
                {
                    wktBuilder.Append(points[r][i].ToString());
                    if (i != points[r].Count - 1)
                    {
                        wktBuilder.Append(",");
                    }
                }
                wktBuilder.Append(")");
                if (r != points.Count - 1)
                {
                    wktBuilder.Append(",");
                }
            }
            wktBuilder.Append(")");

            return wktBuilder.ToString();
        }
    }
}
