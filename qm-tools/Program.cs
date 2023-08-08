using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using System.CommandLine;
using Terrain.Tiles;

namespace cli;

internal class Program
{
    private const int MAX = 32767;

    static async Task<int> Main(string[] args)
    {
        Console.WriteLine("qm-tools 0.1");

        // info command
        var infoCommand = new Command("info", "Gives info of terrain tile");

        var inputOption = new Option<FileInfo?>(
            aliases: new[] { "--input", "-i" },
            description: "input terrain tile")
        { IsRequired = true };
        infoCommand.AddOption(inputOption);

        infoCommand.SetHandler((input) =>
        {
            ReadFile(input!);
        }, inputOption);

        
        var rootCommand = new RootCommand("qm-tools - quantized mesh tools");
        rootCommand.AddCommand(infoCommand);

        return await rootCommand.InvokeAsync(args);
    }

    static void GetJSON()
    {

    }

    static bool IsGZipped(FileInfo file)
    {
        var byteStream = File.OpenRead(file.FullName);
        var firstbyte = byteStream.ReadByte();
        var secondByte = byteStream.ReadByte();
        byteStream.Close();
        if (firstbyte == 0x1f && secondByte == 0x8b)
        {
            return true;
        }
        return false;
    }

    static void ReadFile(FileInfo file)
    {
        Console.WriteLine("Reading file: " + file.FullName);

        if (file != null && File.Exists(file.FullName))
        {
            var isGzipped = IsGZipped(file);
            Console.WriteLine("Gzipped file: " + isGzipped);

            if(isGzipped)
            {
                Console.WriteLine("The file is Gzipped, decompress first. End of program");
                Environment.Exit(1);
            }

            var pbfStream = File.OpenRead(file.FullName);
            var terrainTile = TerrainTileParser.Parse(pbfStream);

            Console.WriteLine("CenterX in ECEF: " + terrainTile.Header.CenterX);
            Console.WriteLine("CenterY in ECEF: " + terrainTile.Header.CenterY);
            Console.WriteLine("CenterZ in ECEF: " + terrainTile.Header.CenterZ);

            Console.WriteLine("Number of vertices: " + terrainTile.VertexData.vertexCount);
            Console.WriteLine("Numbre of triangles: " + terrainTile.IndexData16.indices.Length / 3);
            Console.WriteLine("Minimum height: " + terrainTile.Header.MinimumHeight);
            Console.WriteLine("Maximum height: " + terrainTile.Header.MaximumHeight);
            Console.WriteLine("Has normals extension: " + terrainTile.HasNormals);
            Console.WriteLine("Has watermask extension: " + terrainTile.HasWatermask);
            Console.WriteLine("Has metadata extension: " + terrainTile.HasMetadata);
            Console.WriteLine($"Minimum height: {terrainTile.Header.MinimumHeight}");
            Console.WriteLine($"Maximum height: {terrainTile.Header.MaximumHeight}");
        }
        else
        {
            Console.WriteLine("File not found");
        }
    }

    private static Feature GetFeature(Triangle t)
    {
        var p0 = t.p0.ToGeoJsonPoint();
        var p1 = t.p1.ToGeoJsonPoint();
        var p2 = t.p2.ToGeoJsonPoint();

        var height_average = (p0.Altitude + p1.Altitude + p2.Altitude) / 3;

        var coordinates = new List<Position>() { p0, p1, p2, p0 };
        var polygon = new Polygon(new List<LineString> { new LineString(coordinates) });
        var featureProperties = new Dictionary<string, object> { { "Height", height_average } };
        var feature = new Feature(polygon, featureProperties);
        return feature;
    }
}


public static class PointExtension
{
    public static Position ToGeoJsonPoint(this Wkx.Point p)
    {
        return new Position((double)p.Y, (double)p.X, p.Z);
    }
}
