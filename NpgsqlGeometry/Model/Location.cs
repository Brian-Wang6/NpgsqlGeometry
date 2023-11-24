using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NpgsqlGeometry.Model
{
    [Table("LS_LocationMapRegion")]
    public class Location
    {
        [Key]
        public long LocationUID { get; set; }
        
        [Column(TypeName = "geometry")]
        public Geometry? RegionCoords { get; set; }
    }
}
