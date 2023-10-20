using NUnit.Framework;
using Terrain.Tiles;

namespace tests;
public class TerrainTileWriterTests
{
    [Test]
    public void FirstWriteTest() { 
        // arrange
        const string firstTerrainFile = "data/9_533_383.terrain";
        var pbfStream = File.OpenRead(firstTerrainFile);
        var terrainTile = TerrainTileParser.Parse(pbfStream);

        // act
        var bytes = terrainTile.AsBinary();
        // check bytes is not null
        Assert.IsTrue(bytes != null);
    }
}
