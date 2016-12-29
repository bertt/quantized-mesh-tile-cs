using NUnit.Framework;

namespace Terrain.Tile.Tests
{
    public class GlobalGeodeticTests
    {
        [Test]
        public void GetTileBoundsTestsLevel0()
        {
            // act
            var bounds = GlobalGeodetic.GetTileBounds(0, 0, 0);

            // assert
            Assert.IsTrue(bounds[0] == -180);
            Assert.IsTrue(bounds[1] == -90);
            Assert.IsTrue(bounds[2] == 180);
            Assert.IsTrue(bounds[3] == 90);
        }

        [Test]
        public void GetTileBoundsTestsLevel1LL()
        {
            // act LL
            var bounds = GlobalGeodetic.GetTileBounds(0, 0, 1);

            // assert
            Assert.IsTrue(bounds[0] == -180);
            Assert.IsTrue(bounds[1] == -90);
            Assert.IsTrue(bounds[2] == 0);
            Assert.IsTrue(bounds[3] == 0);
        }

        [Test]
        public void GetTileBoundsTestsLevel1UL()
        {
            // act UL
            var bounds = GlobalGeodetic.GetTileBounds(0, 1, 1);

            // assert
            Assert.IsTrue(bounds[0] == -180);
            Assert.IsTrue(bounds[1] == 0);
            Assert.IsTrue(bounds[2] == 0);
            Assert.IsTrue(bounds[3] == 90);
        }

        [Test]
        public void GetTileBoundsTestsLevel1LR()
        {
            // act LR
            var bounds = GlobalGeodetic.GetTileBounds(1, 0, 1);

            // assert
            Assert.IsTrue(bounds[0] == 0);
            Assert.IsTrue(bounds[1] == -90);
            Assert.IsTrue(bounds[2] == 180);
            Assert.IsTrue(bounds[3] == 0);
        }

        [Test]
        public void GetTileBoundsTestsLevel1UR()
        {
            // act UR
            var bounds = GlobalGeodetic.GetTileBounds(1, 1, 1);

            // assert
            Assert.IsTrue(bounds[0] == 0);
            Assert.IsTrue(bounds[1] == 0);
            Assert.IsTrue(bounds[2] == 180);
            Assert.IsTrue(bounds[3] == 90);
        }

        [Test]
        public void GetTileBoundsTestsLevel2()
        {
            // act
            var bounds = GlobalGeodetic.GetTileBounds(1, 2, 2);

            // assert
            Assert.IsTrue(bounds[0] == -90);
            Assert.IsTrue(bounds[1] == 0);
            Assert.IsTrue(bounds[2] == 0);
            Assert.IsTrue(bounds[3] == 45);
        }

    }

}
