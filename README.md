# quantized-mesh-tile-cs

[![NuGet Status](http://img.shields.io/nuget/v/quantized-mesh-tile.svg?style=flat)](https://www.nuget.org/packages/quantized-mesh-tile/)

A .NET Standard 2.0 library for decoding a terrain tile (quantized mesh format).

More info about the quantized mesh format: https://github.com/AnalyticalGraphicsInc/quantized-mesh

For more awesome quantized mesh implementations see https://github.com/bertt/awesome-quantized-mesh-tiles

### Installation

```
PM> Install-Package quantized-mesh-tile
```

### Usage: Decoding

```csharp
const string terrainTileUrl = @"https://geodan.github.io/terrain/samples/heuvelrug/tiles/13/8432/6467.terrain";

var client = new HttpClient();
var stream = await client.GetStreamAsync(terrainTileUrl);
var terrainTile = TerrainTileParser.Parse(stream);
Console.WriteLine("Number of vertices: " + terrainTile.VertexData.vertexCount);
Console.ReadLine();
```

### Usage: Encoding

Encode a list of WKT `POLYGON Z` triangles into a quantized-mesh tile byte stream.
The tile coordinates `(z, x, y)` follow the TMS scheme and determine the geographic extent.

```csharp
var triangles = new List<string>
{
    "POLYGON Z ((7.38 44.65 303, 7.38 45.0 320, 7.56 44.82 310, 7.38 44.65 303))",
    "POLYGON Z ((7.38 44.65 303, 7.73 44.65 350, 7.56 44.82 310, 7.38 44.65 303))"
};

byte[] bytes = TerrainTileEncoder.Encode(triangles, z: 9, x: 533, y: 383);
File.WriteAllBytes("output.terrain", bytes);
```

The encoder:
- Deduplicates vertices and quantizes coordinates to `[0, 32767]`
- Computes the tile header (ECEF center, bounding sphere, height range)
- Identifies edge vertices (west, south, east, north borders)
- Serializes all sections to the quantized-mesh binary format

### Benchmark

```
|                    Method |     Mean |    Error |   StdDev |
|-------------------------- |---------:|---------:|---------:|
| ParseVectorTileFromStream | 46.74 us | 0.099 us | 0.087 us |
```


 ### Sample: convert to GeoJSON

 See samples/qm2geojson, sample code for converting a quantized mesh tile to GeoJSON. 
 
 Result: see https://github.com/bertt/quantized-mesh-tile-cs/blob/master/samples/qm2geojson/triangles.geojson
 
 Sample visualization:

![heightmap](https://user-images.githubusercontent.com/538812/66191533-dbddc700-e68e-11e9-8a62-e190353c8b90.png)

### History

26-04-22: release version 0.5 with quantized-mesh tile encoding support

23-05-26: release version 0.4 to .NET 6

18-12-28: release version 0.3 with BinaryReader instead of FastBinaryReader

18-12-05: release version 0.2 with new .NET project file and conversion to WGS84 (method GetTriangles) removed.


