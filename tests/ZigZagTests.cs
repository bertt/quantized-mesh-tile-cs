using NUnit.Framework;

namespace Terrain.Tiles.Tests;

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
        Assert.IsTrue(ZigZag.Encode(-1) ==1 );
        Assert.IsTrue(ZigZag.Encode(-2) == 3);
        Assert.IsTrue(ZigZag.Encode(0) == 0);
        Assert.IsTrue(ZigZag.Encode(1) == 2);
        Assert.IsTrue(ZigZag.Encode(2) == 4);
        Assert.IsTrue(ZigZag.Decode(1) == -1);
        Assert.IsTrue(ZigZag.Decode(3) == -2);
        Assert.IsTrue(ZigZag.Decode(0) == 0);
        Assert.IsTrue(ZigZag.Decode(4) == 2);
        Assert.IsTrue(ZigZag.Decode(65534) == 32767);
        Assert.IsTrue(ZigZag.Decode(32765) == -16383);
        Assert.IsTrue(ZigZag.Decode(32766) == 16383);
        Assert.IsTrue(ZigZag.Decode(32767) == -16384);
        Assert.IsTrue(ZigZag.Encode(-16383) == 32765);
    }



    [Test]
    public void TestZigZagEncodeDecode()
    {
        // arrange
        const int inputVar = 65534;

        // act
        var res = ZigZag.Decode(inputVar);
        var res1 = ZigZag.Encode(res);

        // assert
        Assert.IsTrue(res == 32767);
        Assert.IsTrue(res1 == inputVar);
    }

    [Test]
    public void TestZigZagEncodeDecode2()
    {
        // arrange
        ushort  inputVar = 32765;

        var res = ZigZag.Decode(inputVar);
        Assert.IsTrue(res == -16383);
        var res1 = ZigZag.Encode(-16383);

        // assert
        Assert.IsTrue(res1 == inputVar);
    }

}
