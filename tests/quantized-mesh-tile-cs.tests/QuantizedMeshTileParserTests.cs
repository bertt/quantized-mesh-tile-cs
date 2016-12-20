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

            // check headers
            Assert.IsTrue(qmt.Header.CenterX == 4492197.38443436);
            Assert.IsTrue(qmt.Header.CenterY == 596134.0874495716);
            Assert.IsTrue(qmt.Header.CenterZ == 4473851.280340988);
            Assert.IsTrue(qmt.Header.MinimumHeight == 206.3408660888672);
            Assert.IsTrue(qmt.Header.MaximumHeight == 787.9429931640625);
            Assert.IsTrue(qmt.Header.BoundingSphereCenterX == 4492197.38443436);
            Assert.IsTrue(qmt.Header.BoundingSphereCenterY == 596134.0874495716);
            Assert.IsTrue(qmt.Header.BoundingSphereCenterZ == 4473851.280340988);
            Assert.IsTrue(qmt.Header.BoundingSphereRadius == 24100.511082898243);
            Assert.IsTrue(qmt.Header.HorizonOcclusionPointX == 0.5);
            Assert.IsTrue(qmt.Header.HorizonOcclusionPointY == 0.5);
            Assert.IsTrue(qmt.Header.HorizonOcclusionPointZ == 0.5);

            // check vertexdata
            Assert.IsTrue(qmt.VertexData.vertexCount == 103);
            Assert.IsTrue(qmt.VertexData.u[0] == 21075);
            Assert.IsTrue(qmt.VertexData.v[0] == 26684);
            Assert.IsTrue(qmt.VertexData.height[0] == 543);

            // check IndexData
            Assert.IsTrue(qmt.IndexData16.indices.Length == 381);
            Assert.IsTrue(qmt.IndexData16.indices[0] == 0);
            Assert.IsTrue(qmt.IndexData16.indices[qmt.IndexData16.indices.Length - 1] == 100);
            Assert.IsTrue(qmt.IndexData16.indices.Length / 3 == qmt.IndexData16.triangleCount);

            // check EdgeIndices16 
            Assert.IsTrue(qmt.EdgeIndices16.eastIndices.Length == 10);
            Assert.IsTrue(qmt.EdgeIndices16.eastIndices[0] == 13);
            Assert.IsTrue(qmt.EdgeIndices16.eastIndices[9] == 102);
            Assert.IsTrue(qmt.EdgeIndices16.westIndices.Length == 30);
            Assert.IsTrue(qmt.EdgeIndices16.westIndices[0] == 19);
            Assert.IsTrue(qmt.EdgeIndices16.westIndices[29] == 96);
            Assert.IsTrue(qmt.EdgeIndices16.northIndices.Length == 25);
            Assert.IsTrue(qmt.EdgeIndices16.northIndices[0] == 1);
            Assert.IsTrue(qmt.EdgeIndices16.northIndices[24] == 102);
            Assert.IsTrue(qmt.EdgeIndices16.southIndices.Length == 14);
            Assert.IsTrue(qmt.EdgeIndices16.southIndices[0] == 47);
            Assert.IsTrue(qmt.EdgeIndices16.southIndices[13] == 85);
        }

        [Test]
        public void TestWatermarkTileParsing()
        {
            // arrange
            const string firstTerrainFile = "Quantized.Mesh.Tile.Tests.data.9_769_319_watermask.terrain";

            // act
            var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(firstTerrainFile);
            var qmt = QuantizedMeshTileParser.Parse(pbfStream);

            // todo: check extensions
            //Assert.IsTrue(qmt.NormalExtensionData.vertexCount == 4);
            //Assert.IsTrue(qmt.NormalExtensionData.xy.Length == 8);
            //Assert.IsTrue(qmt.NormalExtensionData.xy[0] == 0);
            //Assert.IsTrue(qmt.NormalExtensionData.xy[7] == 203);
        }


        [Test]
        public void TestAnotherTileParsing()
        {
            // arrange
            const string firstTerrainFile = "Quantized.Mesh.Tile.Tests.data.10_1563_590_light_watermask.terrain";

            // act
            var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(firstTerrainFile);
            var qmt = QuantizedMeshTileParser.Parse(pbfStream);

            // todo: check extensions
        }


    }
}