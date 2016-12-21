
using NUnit.Framework;

namespace Terrain.Tile.Tests
{
    public class GlobalGeodeticTests
    {
        [Test]
        public void GetTileBoundsTests()
        {
            // act
            var bounds = GlobalGeodetic.GetTileBounds(0, 0, 0);

            // assert
            Assert.IsTrue(bounds[0] == -180);
            Assert.IsTrue(bounds[1] == -90);
            Assert.IsTrue(bounds[2] == 0);
            Assert.IsTrue(bounds[3] == 90);

        }
    }
}
