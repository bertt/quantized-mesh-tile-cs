using System;
using System.Collections.Generic;
using System.Linq;

namespace Terrain.Tiles;

public static class TerrainTileEncoder
{
    private const double MaxQuantized = 32767.0;
    private const double WGS84A = 6378137.0;
    private const double WGS84E2 = 0.00669437999014;
    private const double DegToRad = Math.PI / 180.0;

    /// <summary>
    /// Encodes a set of WKT POLYGON Z triangles into a quantized-mesh tile byte stream.
    /// </summary>
    /// <param name="wktTriangles">List of WKT POLYGON Z strings, each describing one triangle.</param>
    /// <param name="z">Tile zoom level.</param>
    /// <param name="x">Tile column (TMS scheme).</param>
    /// <param name="y">Tile row (TMS scheme).</param>
    public static byte[] Encode(List<string> wktTriangles, int z, int x, int y)
    {
        var (lonMin, lonMax, latMin, latMax) = GetTileBounds(z, x, y);

        var triangles = wktTriangles.Select(ParseWktTriangle).ToList();

        double minHeight = triangles.SelectMany(t => t).Min(c => c.Height);
        double maxHeight = triangles.SelectMany(t => t).Max(c => c.Height);

        // Deduplicate vertices, preserving insertion order for high-watermark encoding.
        var vertexIndexMap = new Dictionary<(int u, int v, int h), int>();
        var vertexList = new List<(int u, int v, int h)>();
        var indexBuffer = new List<ushort>();

        foreach (var triangle in triangles)
        {
            foreach (var coord in triangle)
            {
                int u = Quantize(coord.X, lonMin, lonMax);
                int v = Quantize(coord.Y, latMin, latMax);
                int h = Quantize(coord.Height, minHeight, maxHeight);

                var key = (u, v, h);
                if (!vertexIndexMap.TryGetValue(key, out int idx))
                {
                    idx = vertexList.Count;
                    vertexIndexMap[key] = idx;
                    vertexList.Add(key);
                }
                indexBuffer.Add((ushort)idx);
            }
        }

        var vertexData = new VertexData
        {
            vertexCount = (uint)vertexList.Count,
            u = vertexList.Select(v => v.u).ToArray(),
            v = vertexList.Select(v => v.v).ToArray(),
            height = vertexList.Select(v => v.h).ToArray()
        };

        var indexData16 = new IndexData16
        {
            triangleCount = (uint)(indexBuffer.Count / 3),
            indices = indexBuffer.ToArray()
        };

        var edgeIndices = BuildEdgeIndices(vertexList);
        var header = BuildHeader(lonMin, lonMax, latMin, latMax, (float)minHeight, (float)maxHeight);

        var tile = new TerrainTile
        {
            Header = header,
            VertexData = vertexData,
            IndexData16 = indexData16,
            EdgeIndices16 = edgeIndices
        };

        return tile.AsBinary();
    }

    private static (double lonMin, double lonMax, double latMin, double latMax) GetTileBounds(int z, int x, int y)
    {
        double tileWidth = 360.0 / Math.Pow(2, z + 1);
        double tileHeight = 180.0 / Math.Pow(2, z);
        double lonMin = -180.0 + x * tileWidth;
        double latMin = -90.0 + y * tileHeight;
        return (lonMin, lonMin + tileWidth, latMin, latMin + tileHeight);
    }

    private static int Quantize(double value, double min, double max)
    {
        if (max <= min) return 0;
        double normalized = (value - min) / (max - min);
        return (int)Math.Round(Math.Max(0.0, Math.Min(1.0, normalized)) * MaxQuantized);
    }

    private static EdgeIndices16 BuildEdgeIndices(List<(int u, int v, int h)> vertices)
    {
        const int edge = (int)MaxQuantized;

        var west = new List<ushort>();
        var south = new List<ushort>();
        var east = new List<ushort>();
        var north = new List<ushort>();

        for (int i = 0; i < vertices.Count; i++)
        {
            var (u, v, _) = vertices[i];
            if (u == 0) west.Add((ushort)i);
            if (v == 0) south.Add((ushort)i);
            if (u == edge) east.Add((ushort)i);
            if (v == edge) north.Add((ushort)i);
        }

        return new EdgeIndices16
        {
            westVertexCount = (uint)west.Count,
            westIndices = west.ToArray(),
            southVertexCount = (uint)south.Count,
            southIndices = south.ToArray(),
            eastVertexCount = (uint)east.Count,
            eastIndices = east.ToArray(),
            northVertexCount = (uint)north.Count,
            northIndices = north.ToArray()
        };
    }

    private static TerrainTileHeader BuildHeader(
        double lonMin, double lonMax, double latMin, double latMax,
        float minHeight, float maxHeight)
    {
        double centerLon = (lonMin + lonMax) / 2.0;
        double centerLat = (latMin + latMax) / 2.0;
        double centerHeight = (minHeight + maxHeight) / 2.0;

        var (cx, cy, cz) = ToEcef(centerLon, centerLat, centerHeight);

        // Bounding sphere: compute radius as max ECEF distance from center to tile corners.
        double radius = new[]
        {
            (lonMin, latMin), (lonMax, latMin),
            (lonMin, latMax), (lonMax, latMax)
        }
        .Select(corner =>
        {
            var (ex, ey, ez) = ToEcef(corner.Item1, corner.Item2, centerHeight);
            double dx = ex - cx, dy = ey - cy, dz = ez - cz;
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        })
        .Max();

        // Simplified horizon occlusion point: normalize center direction on scaled ellipsoid.
        double len = Math.Sqrt(cx * cx + cy * cy + cz * cz);
        double hopX = len > 0 ? cx / len : 0.5;
        double hopY = len > 0 ? cy / len : 0.5;
        double hopZ = len > 0 ? cz / len : 0.5;

        return new TerrainTileHeader
        {
            CenterX = cx,
            CenterY = cy,
            CenterZ = cz,
            MinimumHeight = minHeight,
            MaximumHeight = maxHeight,
            BoundingSphereCenterX = cx,
            BoundingSphereCenterY = cy,
            BoundingSphereCenterZ = cz,
            BoundingSphereRadius = radius,
            HorizonOcclusionPointX = hopX,
            HorizonOcclusionPointY = hopY,
            HorizonOcclusionPointZ = hopZ
        };
    }

    private static (double x, double y, double z) ToEcef(double lonDeg, double latDeg, double heightM)
    {
        double lon = lonDeg * DegToRad;
        double lat = latDeg * DegToRad;
        double sinLat = Math.Sin(lat);
        double cosLat = Math.Cos(lat);
        double N = WGS84A / Math.Sqrt(1 - WGS84E2 * sinLat * sinLat);
        double x = (N + heightM) * cosLat * Math.Cos(lon);
        double y = (N + heightM) * cosLat * Math.Sin(lon);
        double z = (N * (1 - WGS84E2) + heightM) * sinLat;
        return (x, y, z);
    }

    /// <summary>
    /// Parses a WKT POLYGON Z string into a list of three coordinates (triangle).
    /// Expected format: POLYGON Z ((x1 y1 z1, x2 y2 z2, x3 y3 z3, x1 y1 z1))
    /// </summary>
    private static List<Coordinate> ParseWktTriangle(string wkt)
    {
        int start = wkt.IndexOf('(');
        int end = wkt.LastIndexOf(')');
        string inner = wkt.Substring(start + 1, end - start - 1).Trim('(', ')');

        var points = inner.Split(',')
            .Select(p => p.Trim().Split(' '))
            .Where(parts => parts.Length >= 3)
            .Select(parts => new Coordinate(
                double.Parse(parts[0], System.Globalization.CultureInfo.InvariantCulture),
                double.Parse(parts[1], System.Globalization.CultureInfo.InvariantCulture),
                double.Parse(parts[2], System.Globalization.CultureInfo.InvariantCulture)))
            .ToList();

        // WKT polygons close with a duplicate of the first point; take only the 3 unique vertices.
        if (points.Count > 3) points = points.Take(3).ToList();
        return points;
    }
}
