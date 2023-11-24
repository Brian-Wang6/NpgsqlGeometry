using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;
using NpgsqlGeometry.DBContext;
using NpgsqlGeometry.Model;
using NpgsqlGeometry.Serivce.Interface;

namespace NpgsqlGeometry.Serivce
{
    public class LocationService : ILocationService
    {
        private readonly PostgreDBContext postgresDBContext;

        public LocationService(PostgreDBContext postgresDBContext)
        {
            this.postgresDBContext = postgresDBContext;
        }

        public async Task<List<Location>> GetLocationsAsync()
        {
            var locations = await postgresDBContext.Locations.ToListAsync();

            return locations;
        }

        public async Task<Location> GetLocationByIDAsync(long id)
        {
            var location = await postgresDBContext.Locations.FirstOrDefaultAsync(l => l.LocationUID == id);
            return location ?? new Location();
        }

        public async Task SaveLocationAsync(Location location)
        {
            await postgresDBContext.Locations.AddAsync(location);
            var res = await postgresDBContext.SaveChangesAsync();

        }
    }
}
