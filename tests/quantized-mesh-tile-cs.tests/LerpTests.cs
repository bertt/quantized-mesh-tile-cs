using NUnit.Framework;

namespace Terrain.Tiles.Tests
{
    public class LerpTests
    {
        [Test]
        public void LerpTest()
        {
            Assert.IsTrue(Mathf.Lerp(10.0, 30.0, 0.5) == 20);
            Assert.IsTrue(Mathf.Lerp(10.0, 30.0, 0.0) == 10);
        }
    }
}
