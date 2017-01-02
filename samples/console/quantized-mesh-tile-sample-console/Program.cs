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

            const string terrainTileUrl = "http://assets.agi.com/stk-terrain/v1/tilesets/world/tiles/0/0/0.terrain";

            var gzipWebClient = new HttpClient(new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            });
            var bytes = gzipWebClient.GetByteArrayAsync(terrainTileUrl).Result;

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
    }
}
