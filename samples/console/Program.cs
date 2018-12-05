using System;
using System.IO;
using System.Net.Http;
using Terrain.Tiles;

namespace quantized_mesh_tile_sample_console
{
    class Program
    {
        static void Main(string[] args)
        {
            const string terrainTileUrl = @"https://maps.tilehosting.com/data/terrain-quantized-mesh/9/536/391.terrain?key=wYrAjVu6bV6ycoXliAPl";

            var client = new HttpClient();
            var bytes = client.GetByteArrayAsync(terrainTileUrl).Result;
            var stream = new MemoryStream(bytes);

            var terrainTile = TerrainTileParser.Parse(stream);
            var triangles = terrainTile.GetTriangles(536, 391, 9);

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
