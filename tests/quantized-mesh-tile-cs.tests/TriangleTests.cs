using NUnit.Framework;

namespace Terrain.Tiles.Tests
{
    public class TriangleTests
    {
        [Test]
        public void TriangleTest()
        {
            // arrange
            var c1 = new Coordinate(0, 0, 0);
            var c2 = new Coordinate(5, 5, 0);
            var c3 = new Coordinate(10, 0, 0);

            // act
            var t = new Triangle(c1, c2, c3);

            // assert
            Assert.IsTrue(t != null);
        }
    }
}
