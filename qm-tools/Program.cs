using System.CommandLine;
using Terrain.Tiles;

namespace cli;

internal class Program
{
    private const int MAX = 32767;

    static async Task<int> Main(string[] args)
    {
        Console.WriteLine("qm-tools");
        if (args.Length == 0)
        {
            args = new string[] { "-h" };
        }

        var fileOption = new Option<FileInfo?>(
            name: "-i",
            description: "The file to read and display on the console.");

        var rootCommand = new RootCommand("qm-tools - quantized mesh tools");
        rootCommand.AddOption(fileOption);

        rootCommand.SetHandler((file) =>
        {
            ReadFile(file!);
        },
            fileOption);

        return await rootCommand.InvokeAsync(args);
    }

    static void ReadFile(FileInfo file)
    {
        if (file != null)
        {
            var pbfStream = File.OpenRead(file.FullName);
            var terrainTile = TerrainTileParser.Parse(pbfStream);

            Console.WriteLine("Number of vertices: " + terrainTile.VertexData.vertexCount);
            Console.WriteLine("Numbre of triangles: " + terrainTile.IndexData16.indices.Length/3);
            Console.WriteLine("Minimum height: " + terrainTile.Header.MinimumHeight);
            Console.WriteLine("Maximum height: " + terrainTile.Header.MaximumHeight);
            Console.WriteLine("Has normals extension: " + terrainTile.HasNormals);
            Console.WriteLine("Has watermask extension: " + terrainTile.HasWatermask);
            Console.WriteLine("Has metadata extension: " + terrainTile.HasMetadata);

            Console.WriteLine("Vertices (u, v, h)");
            Console.WriteLine($"u,v on scale 0 - {MAX}");

            for (var n = 0; n < terrainTile.VertexData.vertexCount; n++)
            {
                var u = terrainTile.VertexData.u[n]; //32767
                var v = terrainTile.VertexData.v[n]; // 0
                var h = terrainTile.VertexData.height[n]; //26707
                var h1 = Mathf.Lerp(terrainTile.Header.MinimumHeight, terrainTile.Header.MaximumHeight, (double)h / MAX); //2754

                Console.WriteLine($"Vertices (u, v, h): {u}, {v}, {h1}");
            }
        }
    }
}
