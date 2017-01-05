using NUnit.Framework;
using Terrain.BruTile;

namespace Terrain.Tiles.Tests
{
    public class BruTileTests
    {
        [Test]
        public void GetTileBoundsTest()
        {
            // arrange
            var level = 0;
            var col = 0;
            var row = 0;
            var schema = new TmsTileSchema();


            // act
            var tileRange = new TileRange(col, row);
            var extent = TileTransform.TileToWorld(tileRange, level.ToString(), schema);

            // assert
            Assert.IsTrue(extent.MinX == -180);
            Assert.IsTrue(extent.MaxX == 0);
            Assert.IsTrue(extent.MinY == -90);
            Assert.IsTrue(extent.MaxY == 90);
        }

    }
}
