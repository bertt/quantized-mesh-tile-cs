using System;

namespace Terrain.Tile
{
    public class GlobalGeodetic
    {
        public static double GetNumberOfTiles(int level)
        {
            return Math.Pow(2, level);
        }

        public static double TileToLon(int x, int level)
        {
            return x * 360 / GetNumberOfTiles(level) - 180;
        }

        public static double TileToLat(int y, int level)
        {
            return y * 180 / GetNumberOfTiles(level) - 90;
        }

        public static double[] GetTileBounds(int tx, int ty, int level)
        {
            var x0 = TileToLon(tx, level);
            var x1 = TileToLon(tx + 1, level);
            var y0 = TileToLat(ty, level);
            var y1 = TileToLat(ty + 1, level);

            return new double[4] { x0, y0, x1, y1 };
        }
    }
}
