using System;
using System.IO;
using System.Net;
using System.Net.Http;
using Terrain.Tiles;

namespace quantized_mesh_tile_sample_console
{
    class Program
    {
        static void Main(string[] args)
        {
            const string terrainTileUrl = "https://assets.cesium.com/1/0/0/0.terrain?v=1.1.0";

            var cesiumClient = GetCesiumWebClient();
            var bytes = cesiumClient.GetByteArrayAsync(terrainTileUrl).Result;

            var stream = new MemoryStream(bytes);

            var terrainTile = TerrainTileParser.Parse(stream);
            var triangles = terrainTile.GetTriangles(0, 0, 0);
            var count = triangles.Count;
            var first_x = triangles[0].Coordinate1.X;
            var first_y = triangles[0].Coordinate1.Y;
            var first_z = triangles[0].Coordinate1.Height;

            Console.WriteLine($"Number of triangles: {count}");
            Console.WriteLine($"Coordinates first triangle, first vertice: {first_x}, {first_y}, {first_z}");
            Console.ReadLine();
        }


        private static HttpClient GetCesiumWebClient()
        {
            var cesiumWebClient = new HttpClient(new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            });
            cesiumWebClient.DefaultRequestHeaders.Add("accept", "application/vnd.quantized-mesh,application/octet-stream;q=0.9,*/*;q=0.01,*/*;access_token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiJkNDhkYmU1My04ZGQxLTQzNDgtOWUzOC05NmM0ZmY3NjU4ODEiLCJpZCI6MjU5LCJhc3NldHMiOnsiMSI6eyJ0eXBlIjoiVEVSUkFJTiIsImV4dGVuc2lvbnMiOlt0cnVlLHRydWUsdHJ1ZV19fSwic3JjIjoiNzkzNTg3YTEtYTk5Yi00ZGQ2LWJiODctMGJjNDMyNmQ1ODUwIiwiaWF0IjoxNTQxNTc4OTMxLCJleHAiOjE1NDE1ODI1MzF9.zZuQxTqsnyOPG_Mzr3-ZBEN7gHEELhvB3FhmzraL6Pg");
            return cesiumWebClient;
        }

    }
}
