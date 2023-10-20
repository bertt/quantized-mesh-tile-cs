using NUnit.Framework;
using Terrain.Tiles;

namespace t;
public  class VertexEncodingTests
{
    [Test]
    public void VertexDecodeTest()
    {
        var items = new ushort[] { 65534, 32765, 32766, 32765, 32767, 0 };
        var expected = new int[] { 32767, 16384, 32767, 16384, 0, 0 };
        var result = VertexData.Decode(items);
        Assert.AreEqual(expected, result);

        var result1 = VertexData.Encode(expected);

        Assert.AreEqual(items[0], result1[0]);
        Assert.AreEqual(items[1], result1[1]);
        // Assert.AreEqual(items, result1);


    }
}
