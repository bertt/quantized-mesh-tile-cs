using CommandLine;
using Terrain.Tiles;

namespace cli;

internal class Program
{
    async static Task Main(string[] args)
    {
        Console.WriteLine("Terrain Tile - CLI");

        Parser.Default.ParseArguments<Options>(args).WithParsed(o =>
        {
            Console.WriteLine($"Input file: {o.Input}");
            var pbfStream = File.OpenRead(o.Input);
            var terrainTile = TerrainTileParser.Parse(pbfStream);

            Console.WriteLine("Number of vertices: " + terrainTile.VertexData.vertexCount);
            Console.WriteLine("Minimum height: " + terrainTile.Header.MinimumHeight);
            Console.WriteLine("Maximum height: " + terrainTile.Header.MaximumHeight);
            Console.WriteLine("Has normals extension: " + terrainTile.HasNormals);
            Console.WriteLine("Has watermask extension: " + terrainTile.HasWatermask);
            Console.WriteLine("Has metadata extension: " + terrainTile.HasMetadata);
        });
    }
}
