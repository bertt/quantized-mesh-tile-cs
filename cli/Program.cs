using Terrain.Tiles;

namespace cli;

internal class Program
{
    async static Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        const string terrainTileUrl = @"https://geodan.github.io/terrain/samples/heuvelrug/tiles/13/8432/6467.terrain";

        var client = new HttpClient();
        var stream = await client.GetStreamAsync(terrainTileUrl);
        var terrainTile = TerrainTileParser.Parse(stream);
        Console.WriteLine("Number of vertices: " + terrainTile.VertexData.vertexCount);
    }
}
