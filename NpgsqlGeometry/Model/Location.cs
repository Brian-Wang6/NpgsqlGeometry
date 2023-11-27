using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NpgsqlGeometry.Model
{
    [Table("LS_LocationMapRegion")]
    public class Location
    {
        [Key]
        public long LocationUID { get; set; }

        [JsonIgnore]
        [Column(TypeName = "geometry")]
        public Geometry? RegionCoords { get; set; }
    }
}
