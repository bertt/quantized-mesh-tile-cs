namespace Terrain.Tile
{
    public static class Mathf
    {
        public static double Lerp(double start, double end, double by)
        {
            return start * by + end * (1 - by);
        }
    }
}
