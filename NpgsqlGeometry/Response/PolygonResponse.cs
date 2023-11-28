namespace NpgsqlGeometry.Response
{
    public class PolygonResponse : BaseResponse
    {
        public List<Polygon> Polygons { get; set; } = new List<Polygon>();
    }

    public class Point
    {
        public Point() { }
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }

        public override string ToString()
        {
            return X + " " + Y;
        }
    }

    public class Polygon
    {
        public long LocationUID { get; set; }
        public List<List<Point>> Points { get; set; } = new List<List<Point>>();
    }
}
