using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using Wkx;

namespace qm_to_geojson
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = 1066;
            var y = 776;
            var level = 10;
            string terrainTileUrl = "https://maps.tilehosting.com/data/terrain-quantized-mesh/{level}/{x}/{y}.terrain?key=rmDrA8qf6zp3pasxyYRn";
            var client = new HttpClient();
            var bytes = client.GetByteArrayAsync(terrainTileUrl).Result;
            var stream = new MemoryStream(bytes);
            var terrainTile = TerrainTileParser.Parse(stream);

            var bounds = GlobalGeodetic.TileBounds(x, y, level);
            var minimumHeight = terrainTile.Header.MinimumHeight; // 934
            var maximumHeight = terrainTile.Header.MaximumHeight; //3167
            var triangleCount = terrainTile.IndexData16.triangleCount; // 2873
            var vertexCount = terrainTile.VertexData.vertexCount; // 959

            var vertices = new List<Wkx.Point>();
            for (var n = 0; n < terrainTile.VertexData.vertexCount; n++)
            {
                var u = terrainTile.VertexData.u[n]; //32767
                var v = terrainTile.VertexData.v[n]; // 0
                var h = terrainTile.VertexData.height[n]; //26707
                var x1 = Mathf.Lerp(bounds[0], bounds[2], ((double)(u) / MAX));
                var y1 = Mathf.Lerp(bounds[1], bounds[3], ((double)(v) / MAX));
                var h1 = Mathf.Lerp(terrainTile.Header.MinimumHeight, terrainTile.Header.MaximumHeight, ((double)h / MAX)); //2754
                var p = new Wkx.Point(x1, y1, h1);
                vertices.Add(p);
            }

            var triangles = new List<Triangle>();

            for (var i = 0; i < terrainTile.IndexData16.indices.Length; i += 3)
            {
                var firstIndex = terrainTile.IndexData16.indices[i];
                var secondIndex = terrainTile.IndexData16.indices[i + 1];
                var thirdIndex = terrainTile.IndexData16.indices[i + 2];

                var p0 = vertices[firstIndex];
                var p1 = vertices[secondIndex];
                var p2 = vertices[thirdIndex];

                var t = new Triangle(p0, p1, p2);
                triangles.Add(t);
            }

        }
    }
}
