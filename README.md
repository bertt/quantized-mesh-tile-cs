# quantized-mesh-tile-cs

[![NuGet Status](http://img.shields.io/nuget/v/quantized-mesh-tile.svg?style=flat)](https://www.nuget.org/packages/quantized-mesh-tile/)

A .NET library for decoding a Cesium terrain tile (format quantized mesh) into a collection of triangles.

The vertices of the triangles contains WGS84 coordinates and height values.

More info about the quantized mesh format: https://github.com/AnalyticalGraphicsInc/quantized-mesh

For more awesome quantized mesh implementations see https://github.com/bertt/awesome-quantized-mesh-tiles

### Installation

```
PM> Install-Package quantized-mesh-tile
```

### Dependents

- 

### Dependencies

NETStandard.Library 1.6.1 https://www.nuget.org/packages/NETStandard.Library/

### Usage

```
            const string terrainTileUrl = @"https://maps.tilehosting.com/data/terrain-quantized-mesh/9/536/391.terrain?key=wYrAjVu6bV6ycoXliAPl";

            var client = new HttpClient();
            var bytes = client.GetByteArrayAsync(terrainTileUrl).Result;
            var stream = new MemoryStream(bytes);

            var terrainTile = TerrainTileParser.Parse(stream);
            var triangles = terrainTile.GetTriangles(536, 391, 9);

            var count = triangles.Count;
            var first_x = triangles[0].Coordinate1.X;
            var first_y = triangles[0].Coordinate1.Y;
            var first_z = triangles[0].Coordinate1.Height;

            Console.WriteLine($"Number of triangles: {count}");
            Console.WriteLine($"Coordinates first triangle, first vertice: {first_x}, {first_y}, {first_z}");
            Console.ReadLine();
```
![wireframe](https://cesiumjs.org/images/2015/12-18/terrain-obb-wireframe.png)
