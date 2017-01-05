using System.Collections.Generic;
using Terrain.BruTile;

namespace Terrain.Tiles
{
    public class TerrainTile
    {
        public TerrainTileHeader Header { get; set; }
        public VertexData VertexData { get; set; }
        public IndexData16 IndexData16 { get; set; }
        public EdgeIndices16 EdgeIndices16 { get; set; }
        // public NormalExtensionData NormalExtensionData { get; set; }
        private const int MAX = 32767;

        public List<Triangle> GetTriangles(int x, int y, int z)
        {
            var schema = new TmsTileSchema();
            var tileRange = new TileRange(x, y);
            var extent = TileTransform.TileToWorld(tileRange, z.ToString(), schema);
            var bounds = new double[] { extent.MinX, extent.MinY, extent.MaxX, extent.MaxY };
            return GetTriangles(bounds);
        }

        private List<Triangle> GetTriangles(double[] bounds)
        {
            var triangles = new List<Triangle>();

            for (var i = 0; i < IndexData16.indices.Length; i += 3)
            {
                var firstIndex = IndexData16.indices[i];
                var secondIndex = IndexData16.indices[i + 1];
                var thirdIndex = IndexData16.indices[i + 2];

                var c1 = GetCoordinate(firstIndex, bounds);
                var c2 = GetCoordinate(secondIndex, bounds);
                var c3 = GetCoordinate(thirdIndex, bounds);
                triangles.Add(new Triangle(c1, c2, c3));
            }
            return triangles;
        }


        private Coordinate GetCoordinate(ushort index, double[] bounds)
        {
            var x = VertexData.u[index];
            var y = VertexData.v[index];
            var height = VertexData.height[index];

            var x1 = Mathf.Lerp(bounds[0], bounds[2], ((double)(x) / MAX));
            var y1 = Mathf.Lerp(bounds[1], bounds[3], ((double)(y) / MAX));

            var h1 = Mathf.Lerp(Header.MinimumHeight, Header.MaximumHeight, ((double)height / MAX));

            return new Coordinate(x1, y1, h1);
        }

    }
}
