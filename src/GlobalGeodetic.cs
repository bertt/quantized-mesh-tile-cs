
using System;

namespace Terrain.Tile
{
    public class GlobalGeodetic
    {
        private const int tileSize = 256;
        private const double resFact = 0.703125;  // equals to: 180/256
        // todo: add support for resFact = 1.4062 // equals to 360/256

        public static double[] GetTileBounds(int tx, int ty, int zoom)
        {
            var res = resFact / Math.Pow(2, zoom);
            var x0 = tx * tileSize * res - 180;
            var y0 = ty * tileSize * res - 90;
            var x1 = ((tx + 1) * tileSize * res) - 180;
            var y1 = ((ty + 1) * tileSize * res) - 90;

            return new double[4] { x0, y0, x1, y1 };
        }
    }
}
