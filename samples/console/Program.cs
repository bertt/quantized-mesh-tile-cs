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
            Console.WriteLine("Number of vertices: " + terrainTile.VertexData.vertexCount);
            Console.ReadLine();
            Console.WriteLine("Press any key to continue...");
        }
    }
}
