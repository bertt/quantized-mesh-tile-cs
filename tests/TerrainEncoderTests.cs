using NUnit.Framework;
using Terrain.Tiles;

namespace tests;

public class TerrainEncoderTests
{
    private static List<string> SampleTriangles => new()
    {
        "POLYGON Z ((7.3828125 44.6484375 303.3, 7.3828125 45.0 320.2, 7.5585937 44.82421875 310.2, 7.3828125 44.6484375 303.3))",
        "POLYGON Z ((7.3828125 44.6484375 303.3, 7.734375 44.6484375 350.3, 7.5585937 44.82421875 310.2, 7.3828125 44.6484375 303.3))",
        "POLYGON Z ((7.734375 44.6484375 350.3, 7.734375 45.0 330.3, 7.5585937 44.82421875 310.2, 7.734375 44.6484375 350.3))",
        "POLYGON Z ((7.734375 45.0 330.3, 7.5585937 44.82421875 310.2, 7.3828125 45.0 320.2, 7.734375 45.0 330.3))"
    };

    [Test]
    public void Encode_ReturnsBytesNotNull()
    {
        var bytes = TerrainTileEncoder.Encode(SampleTriangles, 9, 533, 383);
        Assert.IsNotNull(bytes);
        Assert.IsTrue(bytes.Length > 0);
    }

    [Test]
    public void Encode_CorrectVertexCount()
    {
        var bytes = TerrainTileEncoder.Encode(SampleTriangles, 9, 533, 383);
        var tile = TerrainTileParser.Parse(new MemoryStream(bytes));

        // 4 triangles share 5 unique vertices
        Assert.AreEqual(5u, tile.VertexData.vertexCount);
    }

    [Test]
    public void Encode_CorrectTriangleCount()
    {
        var bytes = TerrainTileEncoder.Encode(SampleTriangles, 9, 533, 383);
        var tile = TerrainTileParser.Parse(new MemoryStream(bytes));

        Assert.AreEqual(4u, tile.IndexData16.triangleCount);
    }

    [Test]
    public void Encode_HeaderHasValidCenter()
    {
        var bytes = TerrainTileEncoder.Encode(SampleTriangles, 9, 533, 383);
        var tile = TerrainTileParser.Parse(new MemoryStream(bytes));

        // ECEF center should be non-zero (tile is near the Alps, not at origin)
        Assert.IsTrue(Math.Abs(tile.Header.CenterX) > 0);
        Assert.IsTrue(Math.Abs(tile.Header.CenterY) > 0);
        Assert.IsTrue(Math.Abs(tile.Header.CenterZ) > 0);
    }

    [Test]
    public void Encode_HeaderHeightRange()
    {
        var bytes = TerrainTileEncoder.Encode(SampleTriangles, 9, 533, 383);
        var tile = TerrainTileParser.Parse(new MemoryStream(bytes));

        Assert.AreEqual(303.3f, tile.Header.MinimumHeight, 0.1f);
        Assert.AreEqual(350.3f, tile.Header.MaximumHeight, 0.1f);
    }

    [Test]
    public void Encode_BoundingSphereRadiusPositive()
    {
        var bytes = TerrainTileEncoder.Encode(SampleTriangles, 9, 533, 383);
        var tile = TerrainTileParser.Parse(new MemoryStream(bytes));

        Assert.IsTrue(tile.Header.BoundingSphereRadius > 0);
    }

    [Test]
    public void Encode_EdgeIndicesPresent()
    {
        var bytes = TerrainTileEncoder.Encode(SampleTriangles, 9, 533, 383);
        var tile = TerrainTileParser.Parse(new MemoryStream(bytes));

        // Vertices on the western and southern tile borders should be identified
        Assert.IsTrue(tile.EdgeIndices16.westVertexCount > 0);
        Assert.IsTrue(tile.EdgeIndices16.southVertexCount > 0);
    }

    [Test]
    public void Encode_CanBeReparsed()
    {
        var bytes = TerrainTileEncoder.Encode(SampleTriangles, 9, 533, 383);
        Assert.DoesNotThrow(() => TerrainTileParser.Parse(new MemoryStream(bytes)));
    }
}
