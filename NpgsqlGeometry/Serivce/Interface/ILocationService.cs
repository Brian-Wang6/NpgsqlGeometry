using NpgsqlGeometry.Model;

namespace NpgsqlGeometry.Serivce.Interface
{
    public interface ILocationService
    {
        Task<List<Location>> GetLocationsAsync();
        Task<Location> GetLocationByIDAsync(long id);
        Task SaveLocationAsync(Response.Polygon polygon);
        Task DeleteLocationAsync(long id);
    }
}
