// Copyright (c) BruTile developers team. All rights reserved. See License.txt in the project root for license information.

namespace Terrain.BruTile
{
    public struct Resolution
    {
        private readonly string _id;
        private readonly double _unitsPerPixel;
        private readonly double _scaleDenominator;
        private readonly double _top;
        private readonly double _left;
        private readonly int _tileWidth;
        private readonly int _tileHeight;
        private readonly int _matrixWidth;
        private readonly int _matrixHeight;

        public Resolution(string id, double unitsPerPixel,
            int tileWidth = 256, int tileHeight = 256,
            double left = 0, double top = 0,
            int matrixWidth = 0, int matrixHeight = 0,
            double scaledenominator = 0)
        {
            _id = id;
            _unitsPerPixel = unitsPerPixel;
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;
            _matrixWidth = matrixWidth;
            _matrixHeight = matrixHeight;
            _top = top;
            _left = left;
            _scaleDenominator = scaledenominator;
        }

        public double UnitsPerPixel
        {
            get { return _unitsPerPixel; }
        }

        public int TileWidth
        {
            get { return _tileWidth; }
        }
    }
}