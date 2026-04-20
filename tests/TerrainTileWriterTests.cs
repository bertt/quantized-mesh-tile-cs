using NUnit.Framework;
using Terrain.Tiles;

namespace tests;

public class TerrainTileWriterTests
{
    [Test]
    public void RoundTrip_ParsedTile_BytesAreNotNull()
    {
        var pbfStream = File.OpenRead("data/9_533_383.terrain");
        var terrainTile = TerrainTileParser.Parse(pbfStream);

        var bytes = terrainTile.AsBinary();

        Assert.IsNotNull(bytes);
        Assert.IsTrue(bytes.Length > 0);
    }

    [Test]
    public void RoundTrip_ParsedTile_HeaderPreserved()
    {
        var pbfStream = File.OpenRead("data/9_533_383.terrain");
        var original = TerrainTileParser.Parse(pbfStream);

        var bytes = original.AsBinary();

        var reparsed = TerrainTileParser.Parse(new MemoryStream(bytes));
        Assert.IsTrue(original.Header.Equals(reparsed.Header));
    }

    [Test]
    public void RoundTrip_ParsedTile_VertexCountPreserved()
    {
        var pbfStream = File.OpenRead("data/9_533_383.terrain");
        var original = TerrainTileParser.Parse(pbfStream);

        var bytes = original.AsBinary();

        var reparsed = TerrainTileParser.Parse(new MemoryStream(bytes));
        Assert.AreEqual(original.VertexData.vertexCount, reparsed.VertexData.vertexCount);
    }

    [Test]
    public void RoundTrip_ParsedTile_TriangleCountPreserved()
    {
        var pbfStream = File.OpenRead("data/9_533_383.terrain");
        var original = TerrainTileParser.Parse(pbfStream);

        var bytes = original.AsBinary();

        var reparsed = TerrainTileParser.Parse(new MemoryStream(bytes));
        Assert.AreEqual(original.IndexData16.triangleCount, reparsed.IndexData16.triangleCount);
    }

    [Test]
    public void RoundTrip_ParsedTile_IndicesPreserved()
    {
        var pbfStream = File.OpenRead("data/9_533_383.terrain");
        var original = TerrainTileParser.Parse(pbfStream);

        var bytes = original.AsBinary();

        var reparsed = TerrainTileParser.Parse(new MemoryStream(bytes));
        Assert.AreEqual(original.IndexData16.indices, reparsed.IndexData16.indices);
    }

    [Test]
    public void RoundTrip_ParsedTile_EdgeIndicesPreserved()
    {
        var pbfStream = File.OpenRead("data/9_533_383.terrain");
        var original = TerrainTileParser.Parse(pbfStream);

        var bytes = original.AsBinary();

        var reparsed = TerrainTileParser.Parse(new MemoryStream(bytes));
        Assert.AreEqual(original.EdgeIndices16.westIndices, reparsed.EdgeIndices16.westIndices);
        Assert.AreEqual(original.EdgeIndices16.southIndices, reparsed.EdgeIndices16.southIndices);
        Assert.AreEqual(original.EdgeIndices16.eastIndices, reparsed.EdgeIndices16.eastIndices);
        Assert.AreEqual(original.EdgeIndices16.northIndices, reparsed.EdgeIndices16.northIndices);
    }
}
