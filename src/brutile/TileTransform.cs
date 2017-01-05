// Copyright (c) BruTile developers team. All rights reserved. See License.txt in the project root for license information.

namespace Terrain.BruTile
{
    public static class TileTransform
    {
        public static Extent TileToWorld(TileRange range, string levelId, ITileSchema schema)
        {
            return TileToWorldNormal(range, levelId, schema);
        }

        private static Extent TileToWorldNormal(TileRange range, string levelId, ITileSchema schema)
        {
            var resolution = schema.Resolutions[levelId];
            var tileWorldUnits = resolution.UnitsPerPixel * schema.GetTileWidth(levelId);
            var minX = range.FirstCol * tileWorldUnits + schema.GetOriginX(levelId);
            var minY = range.FirstRow * tileWorldUnits + schema.GetOriginY(levelId);
            var maxX = (range.FirstCol + range.ColCount) * tileWorldUnits + schema.GetOriginX(levelId);
            var maxY = (range.FirstRow + range.RowCount) * tileWorldUnits + schema.GetOriginY(levelId);
            return new Extent(minX, minY, maxX, maxY);
        }
    }
}
