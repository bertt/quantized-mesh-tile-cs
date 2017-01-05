// Copyright (c) BruTile developers team. All rights reserved. See License.txt in the project root for license information.

namespace Terrain.BruTile
{
    public struct Extent
    {
        public double MinX { get; private set; }
        public double MinY { get; private set; }
        public double MaxX { get; private set; }
        public double MaxY { get; private set; }


        public Extent(double minX, double minY, double maxX, double maxY) : this()
        {
            MinX = minX;
            MinY = minY;
            MaxX = maxX;
            MaxY = maxY;
        }
    }
}