﻿using System.CommandLine;
using Terrain.Tiles;

namespace cli;

internal class Program
{
    static async Task<int> Main(string[] args)
    {
        Console.WriteLine("Terrain Tile - CLI");
        if (args.Length == 0)
        {
            args = new string[] { "-h" };
        }

        var fileOption = new Option<FileInfo?>(
            name: "-i",
            description: "The file to read and display on the console.");

        var rootCommand = new RootCommand("Sample app for System.CommandLine");
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
            var pbfStream = File.OpenRead(file.Name);
            var terrainTile = TerrainTileParser.Parse(pbfStream);

            Console.WriteLine("Number of vertices: " + terrainTile.VertexData.vertexCount);
            Console.WriteLine("Minimum height: " + terrainTile.Header.MinimumHeight);
            Console.WriteLine("Maximum height: " + terrainTile.Header.MaximumHeight);
            Console.WriteLine("Has normals extension: " + terrainTile.HasNormals);
            Console.WriteLine("Has watermask extension: " + terrainTile.HasWatermask);
            Console.WriteLine("Has metadata extension: " + terrainTile.HasMetadata);

        }
    }
}
