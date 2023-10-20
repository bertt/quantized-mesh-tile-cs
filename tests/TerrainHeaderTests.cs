using NUnit.Framework;
using Terrain.Tiles;

namespace tests;
public class TerrainHeaderTests
{
    [Test]
    public void ReadWriteTerrainHeaderTest()
    {
        const string firstTerrainFile = "data/9_533_383.terrain";
        var pbfStream = File.OpenRead(firstTerrainFile);
        var terrainTile = TerrainTileParser.Parse(pbfStream);

        // act
        var headerBinary = terrainTile.Header.AsBinary();

        // assert
        using (var reader = new BinaryReader(new MemoryStream(headerBinary)))
        {
            // read header
            var header = new TerrainTileHeader(reader);

            // assert
            Assert.IsTrue(header.Equals(terrainTile.Header));
        }   
    }
}
