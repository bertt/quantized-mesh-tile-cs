
using NUnit.Framework;
using Terrain.Tiles;

namespace tests;
public class TerrainEncoderTests
{
    [Test]
    public void Encode()
    {
        var z = 9;
        var x=533;
        var y = 383;
        var t0 = "POLYGON Z ((7.3828125 44.6484375 303.3, 7.3828125 45.0 320.2, 7.5585937 44.82421875 310.2, 7.3828125 44.6484375 303.3))";
        var t1 = "POLYGON Z ((7.3828125 44.6484375 303.3, 7.734375 44.6484375 350.3, 7.5585937 44.82421875 310.2, 7.3828125 44.6484375 303.3))";
        var t2 = "POLYGON Z ((7.734375 44.6484375 350.3, 7.734375 45.0 330.3, 7.5585937 44.82421875 310.2, 7.734375 44.6484375 350.3))";
        var t3 = "POLYGON Z ((7.734375 45.0 330.3, 7.5585937 44.82421875 310.2, 7.3828125 45.0 320.2, 7.734375 45.0 330.3))";

        var triangles = new List<string> { t0, t1, t2, t3 };
        var bytes = TerrainTileEncoder.Encode(triangles, z, x, y);
    }
}
