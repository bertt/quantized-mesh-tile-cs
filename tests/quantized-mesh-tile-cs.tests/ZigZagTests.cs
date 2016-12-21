using NUnit.Framework;
using System;

namespace Terrain.Tile.Tests
{

    public class ZigZagTests
    {
        [Test]
        public void TestZigZagDecode()
        {
            // arrange
            const int inputVar = 1;

            // act
            var res = ZigZag.Decode(inputVar);

            // assert
            Assert.IsTrue(res == -1);
        }

        [Test]
        public void AnotherTestZigZagDecode()
        {
            // arrange
            const int inputVar = 3;

            // act
            var res = ZigZag.Decode(inputVar);

            // assert
            Assert.IsTrue(res == -2);
        }

        [Test]
        public void TestZigZagEncode()
        {
            // arrange
            const int inputVar = -2;

            // act
            var res = ZigZag.Encode(inputVar);

            // assert
            Assert.IsTrue(res == 3);
        }

    }
}
