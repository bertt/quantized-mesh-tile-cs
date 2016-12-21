namespace Terrain.Tile
{
    public class Triangle
    {
        public Triangle(Coordinate c1, Coordinate c2, Coordinate c3)
        {
            Coordinate1 = c1;
            Coordinate2 = c2;
            Coordinate3 = c3;
        }

        public Coordinate Coordinate1 { get; set; }
        public Coordinate Coordinate2 { get; set; }
        public Coordinate Coordinate3 { get; set; }
    }
}
