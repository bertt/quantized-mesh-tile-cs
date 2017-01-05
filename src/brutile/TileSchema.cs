// Copyright (c) BruTile developers team. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;

namespace Terrain.BruTile
{
    public class TileSchema : ITileSchema
    {
        public double ProportionIgnored;
        private readonly IDictionary<string, Resolution> _resolutions;

        public TileSchema()
        {
            ProportionIgnored = 0.0001;
            _resolutions = new Dictionary<string, Resolution>();
            YAxis = YAxis.TMS;
            OriginY = Double.NaN;
            OriginX = Double.NaN;
        }

        public double OriginX { get; set; }
        public double OriginY { get; set; }
        public string Srs { get; set; }
        public Extent Extent { get; set; }
        public YAxis YAxis { get; set; }

        public int GetTileWidth(string levelId)
        {
            return Resolutions[levelId].TileWidth;
        }

        public IDictionary<string, Resolution> Resolutions
        {
            get { return _resolutions; }
        }

        public double GetOriginX(string levelId)
        {
            return OriginX;
        }

        public double GetOriginY(string levelId)
        {
            return OriginY;
        }
    }
}