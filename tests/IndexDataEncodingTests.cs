using NUnit.Framework;
using Terrain.Tiles;

namespace tests;

public class IndexDataEncodingTests
{
    [Test]
    public void IndexData16_RoundTrip_TriangleCountPreserved()
    {
        var pbfStream = File.OpenRead("data/9_533_383.terrain");
        var original = TerrainTileParser.Parse(pbfStream);

        var bytes = original.IndexData16.AsBinary();

        using var reader = new BinaryReader(new MemoryStream(bytes));
        var reparsed = new IndexData16(reader);

        Assert.AreEqual(original.IndexData16.triangleCount, reparsed.triangleCount);
    }

    [Test]
    public void IndexData16_RoundTrip_IndicesPreserved()
    {
        var pbfStream = File.OpenRead("data/9_533_383.terrain");
        var original = TerrainTileParser.Parse(pbfStream);

        var bytes = original.IndexData16.AsBinary();

        using var reader = new BinaryReader(new MemoryStream(bytes));
        var reparsed = new IndexData16(reader);

        Assert.AreEqual(original.IndexData16.indices, reparsed.indices);
    }

    [Test]
    public void IndexData16_Encode_WritesCorrectByteLength()
    {
        var pbfStream = File.OpenRead("data/9_533_383.terrain");
        var original = TerrainTileParser.Parse(pbfStream);

        var bytes = original.IndexData16.AsBinary();

        // 4 bytes for triangleCount + triangleCount * 3 * 2 bytes for indices
        int expected = 4 + (int)original.IndexData16.triangleCount * 3 * 2;
        Assert.AreEqual(expected, bytes.Length);
    }

    [Test]
    public void EdgeIndices16_RoundTrip_WestIndicesPreserved()
    {
        var pbfStream = File.OpenRead("data/9_533_383.terrain");
        var original = TerrainTileParser.Parse(pbfStream);

        var bytes = original.EdgeIndices16.AsBinary();

        using var reader = new BinaryReader(new MemoryStream(bytes));
        var reparsed = new EdgeIndices16(reader);

        Assert.AreEqual(original.EdgeIndices16.westIndices, reparsed.westIndices);
    }

    [Test]
    public void EdgeIndices16_RoundTrip_SouthIndicesPreserved()
    {
        var pbfStream = File.OpenRead("data/9_533_383.terrain");
        var original = TerrainTileParser.Parse(pbfStream);

        var bytes = original.EdgeIndices16.AsBinary();

        using var reader = new BinaryReader(new MemoryStream(bytes));
        var reparsed = new EdgeIndices16(reader);

        Assert.AreEqual(original.EdgeIndices16.southIndices, reparsed.southIndices);
    }

    [Test]
    public void EdgeIndices16_RoundTrip_EastIndicesPreserved()
    {
        var pbfStream = File.OpenRead("data/9_533_383.terrain");
        var original = TerrainTileParser.Parse(pbfStream);

        var bytes = original.EdgeIndices16.AsBinary();

        using var reader = new BinaryReader(new MemoryStream(bytes));
        var reparsed = new EdgeIndices16(reader);

        Assert.AreEqual(original.EdgeIndices16.eastIndices, reparsed.eastIndices);
    }

    [Test]
    public void EdgeIndices16_RoundTrip_NorthIndicesPreserved()
    {
        var pbfStream = File.OpenRead("data/9_533_383.terrain");
        var original = TerrainTileParser.Parse(pbfStream);

        var bytes = original.EdgeIndices16.AsBinary();

        using var reader = new BinaryReader(new MemoryStream(bytes));
        var reparsed = new EdgeIndices16(reader);

        Assert.AreEqual(original.EdgeIndices16.northIndices, reparsed.northIndices);
    }
}
