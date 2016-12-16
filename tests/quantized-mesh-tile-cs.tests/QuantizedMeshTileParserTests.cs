using NUnit.Framework;
using System.Reflection;

namespace Quantized.Mesh.Tile.Tests
{
    public class QuantizedMeshTileParserTests 
    {
        [Test]
        public void TestFirstTileParsing()
        {
            // arrange
            const string firstTerrainFile = "Quantized.Mesh.Tile.Tests.data.9_533_383.terrain";

            // act
            var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(firstTerrainFile);
            var qmt = QuantizedMeshTileParser.Parse(pbfStream);
            // assert
            Assert.IsTrue(qmt != null);
            Assert.IsTrue(qmt.QuantizedMeshHeader.CenterX == 7.1132635158129229E+73);
        }

        // testtile (results from python):
        // header:
        // bytesplit: 65636
        // 
        // OrderedDict([('boundingSphereCenterX', 6.091146503455036e+247), ('boundingSphereCenterY', 5.557314938566923e+169), ('boundingSphereCenterZ', 8.030372574023344e+165), ('boundingSphereRadius', 2.763781852442052e+256), ('centerX', 7.113263515812923e+73), ('centerY', 5.5275507747549546e+252), ('centerZ', 1.8009413134742474e+219), ('horizonOcclusionPointX', 1.237560387612898e+214), ('horizonOcclusionPointY', 4.370732370697291e-19), ('horizonOcclusionPointZ', 4.781796516189379e+180), ('maximumHeight', 2.566903154561799e-18), ('minimumHeight', 2.7223549023138454e+20)])

        // maxx: 7.734375
        // maxy: 45.0
        // minx: 7.3828125
        // miny: 44.6484375
    }
}
