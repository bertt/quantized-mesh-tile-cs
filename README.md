# quantized-mesh-tile-cs

[![NuGet Status](http://img.shields.io/nuget/v/quantized-mesh-tile.svg?style=flat)](https://www.nuget.org/packages/quantized-mesh-tile/)

A .NET library for decoding a quantized mesh tile into vertices.

More info about the quantized mesh format: https://github.com/AnalyticalGraphicsInc/quantized-mesh

For more awesome quantized mesh implementations see https://github.com/bertt/awesome-quantized-mesh-tiles

Dependencies: -

###Get it from NuGet 
`
PM> Install-Package quantized-mesh-tile
`

https://www.nuget.org/packages/quantized-mesh-tile

NuGet package contains library for .NET Standard 1.1 

### Sample code:

```
const string terrainTileUrl = "http://assets.agi.com/stk-terrain/v1/tilesets/world/tiles/0/0/0.terrain";

var gzipWebClient = new HttpClient(new HttpClientHandler()
{
    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
});
var bytes = gzipWebClient.GetByteArrayAsync(terrainTileUrl).Result;

var stream = new MemoryStream(bytes);

// act
var qmt = QuantizedMeshTileParser.Parse(stream);
var triangles = terrainTile.GetTriangles(0, 0, 0);

// assert
Assert.IsTrue(qmt != null);
Assert.IsTrue(triangles.Count == 400);
Assert.IsTrue(triangles[0].Coordinate1.X == -180);
Assert.IsTrue(triangles[0].Coordinate1.Y == -78.755149998474081);
Assert.IsTrue(triangles[0].Coordinate1.Height == -55.24706495350631);

Assert.IsTrue(triangles[0].Coordinate2.X == -180);
Assert.IsTrue(triangles[0].Coordinate2.Y == -90);
Assert.IsTrue(triangles[0].Coordinate2.Height == -29.85938702932947);

Assert.IsTrue(triangles[0].Coordinate3.X == -168.75514999847408);
Assert.IsTrue(triangles[0].Coordinate3.Y == -81.56773583178199);
Assert.IsTrue(triangles[0].Coordinate3.Height == -50.34768851199851);
```
![wireframe](https://cesiumjs.org/images/2015/12-18/terrain-obb-wireframe.png)
