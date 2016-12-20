# quantized-mesh-tile-cs

A .NET library for decoding a quantized mesh tile into vertices.

More info about the quantized mesh format: https://github.com/AnalyticalGraphicsInc/quantized-mesh

For more awesome quantized mesh implementations see https://github.com/bertt/awesome-quantized-mesh-tiles

Sample code:

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

// assert
Assert.IsTrue(qmt != null);
Assert.IsTrue(qmt.IndexData16.triangleCount == 400);
```
![wireframe](https://cesiumjs.org/images/2015/12-18/terrain-obb-wireframe.png)
