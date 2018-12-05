# quantized-mesh-tile-cs

[![NuGet Status](http://img.shields.io/nuget/v/quantized-mesh-tile.svg?style=flat)](https://www.nuget.org/packages/quantized-mesh-tile/)

A .NET library (netstandard1.1) for decoding a terrain tile (format quantized mesh).

More info about the quantized mesh format: https://github.com/AnalyticalGraphicsInc/quantized-mesh

For more awesome quantized mesh implementations see https://github.com/bertt/awesome-quantized-mesh-tiles

### Installation

```
PM> Install-Package quantized-mesh-tile
```


### Dependencies

NETStandard.Library 2.0.3 https://www.nuget.org/packages/NETStandard.Library/

### Usage

```
const string terrainTileUrl = @"https://maps.tilehosting.com/data/terrain-quantized-mesh/9/536/391.terrain?key=wYrAjVu6bV6ycoXliAPl";

var client = new HttpClient();
var bytes = client.GetByteArrayAsync(terrainTileUrl).Result;
var stream = new MemoryStream(bytes);

var terrainTile = TerrainTileParser.Parse(stream);
Console.WriteLine("Number of vertices: " + terrainTile.VertexData.vertexCount);
Console.ReadLine();
```
![wireframe](https://cesiumjs.org/images/2015/12-18/terrain-obb-wireframe.png)
