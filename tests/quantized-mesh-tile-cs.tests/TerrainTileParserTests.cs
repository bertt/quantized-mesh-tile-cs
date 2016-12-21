using NUnit.Framework;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;

namespace Terrain.Tile.Tests
{
    public class TerrainTileParserTests
    {
        [Test]
        public void TestFirstTileParsing()
        {
            // arrange
            const string firstTerrainFile = "Terrain.Tile.Tests.data.9_533_383.terrain";

            // act
            var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(firstTerrainFile);
            var terrainTile = TerrainTileParser.Parse(pbfStream);

            // assert
            Assert.IsTrue(terrainTile != null);

            // check headers
            Assert.IsTrue(terrainTile.Header.CenterX == 4492197.38443436);
            Assert.IsTrue(terrainTile.Header.CenterY == 596134.0874495716);
            Assert.IsTrue(terrainTile.Header.CenterZ == 4473851.280340988);
            Assert.IsTrue(terrainTile.Header.MinimumHeight == 206.3408660888672);
            Assert.IsTrue(terrainTile.Header.MaximumHeight == 787.9429931640625);
            Assert.IsTrue(terrainTile.Header.BoundingSphereCenterX == 4492197.38443436);
            Assert.IsTrue(terrainTile.Header.BoundingSphereCenterY == 596134.0874495716);
            Assert.IsTrue(terrainTile.Header.BoundingSphereCenterZ == 4473851.280340988);
            Assert.IsTrue(terrainTile.Header.BoundingSphereRadius == 24100.511082898243);
            Assert.IsTrue(terrainTile.Header.HorizonOcclusionPointX == 0.5);
            Assert.IsTrue(terrainTile.Header.HorizonOcclusionPointY == 0.5);
            Assert.IsTrue(terrainTile.Header.HorizonOcclusionPointZ == 0.5);

            // check vertexdata
            Assert.IsTrue(terrainTile.VertexData.vertexCount == 103);
            Assert.IsTrue(terrainTile.VertexData.u[0] == 21075);
            Assert.IsTrue(terrainTile.VertexData.v[0] == 26684);
            Assert.IsTrue(terrainTile.VertexData.height[0] == 543);

            // check IndexData
            Assert.IsTrue(terrainTile.IndexData16.triangleCount == 127);
            Assert.IsTrue(terrainTile.IndexData16.indices.Length == 381);
            Assert.IsTrue(terrainTile.IndexData16.indices[0] == 0);
            Assert.IsTrue(terrainTile.IndexData16.indices[terrainTile.IndexData16.indices.Length - 1] == 100);
            Assert.IsTrue(terrainTile.IndexData16.indices.Length / 3 == terrainTile.IndexData16.triangleCount);

            // check EdgeIndices16 
            Assert.IsTrue(terrainTile.EdgeIndices16.eastIndices.Length == 10);
            Assert.IsTrue(terrainTile.EdgeIndices16.eastIndices[0] == 13);
            Assert.IsTrue(terrainTile.EdgeIndices16.eastIndices[9] == 102);
            Assert.IsTrue(terrainTile.EdgeIndices16.westIndices.Length == 30);
            Assert.IsTrue(terrainTile.EdgeIndices16.westIndices[0] == 19);
            Assert.IsTrue(terrainTile.EdgeIndices16.westIndices[29] == 96);
            Assert.IsTrue(terrainTile.EdgeIndices16.northIndices.Length == 25);
            Assert.IsTrue(terrainTile.EdgeIndices16.northIndices[0] == 1);
            Assert.IsTrue(terrainTile.EdgeIndices16.northIndices[24] == 102);
            Assert.IsTrue(terrainTile.EdgeIndices16.southIndices.Length == 14);
            Assert.IsTrue(terrainTile.EdgeIndices16.southIndices[0] == 47);
            Assert.IsTrue(terrainTile.EdgeIndices16.southIndices[13] == 85);
        }

        [Test]
        public void TestGetTriangles()
        {
            // arrange
            const string firstTerrainFile = "Terrain.Tile.Tests.data.9_533_383.terrain";
            var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(firstTerrainFile);
            var terrainTile = TerrainTileParser.Parse(pbfStream);

            // act
            var triangles = terrainTile.GetTriangles(533, 383, 9);

            // assert
            Assert.IsTrue(triangles.Count == 127);
            Assert.IsTrue(triangles[0].Coordinate1.Height == 215.97891519320481);
            Assert.IsTrue(triangles[0].Coordinate1.X == 7.608929620502335);
            Assert.IsTrue(triangles[0].Coordinate1.Y == 44.93473449850459);
        }

        [Test]
        public void TestWatermarkTileParsing()
        {
            // arrange
            const string firstTerrainFile = "Terrain.Tile.Tests.data.9_769_319_watermask.terrain";

            // act
            var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(firstTerrainFile);
            var terrainTile = TerrainTileParser.Parse(pbfStream);

            Assert.IsTrue(terrainTile != null);

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
            const string firstTerrainFile = "Terrain.Tile.Tests.data.10_1563_590_light_watermask.terrain";

            // act
            var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(firstTerrainFile);
            var terrainTile = TerrainTileParser.Parse(pbfStream);

            Assert.IsTrue(terrainTile != null);

            // todo: check extensions
        }

        [Test]
        public void TestTerrainTileFromWebParsing()
        {
            // arrange
            const string terrainTileUrl = "http://assets.agi.com/stk-terrain/v1/tilesets/world/tiles/0/0/0.terrain";

            var gzipWebClient = new HttpClient(new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            });
            var bytes = gzipWebClient.GetByteArrayAsync(terrainTileUrl).Result;

            //var fileStream = new FileStream(@"d:/aaa/qm/test1.terrain", FileMode.Create,FileAccess.Write);
            //fileStream.Write(bytes, 0, bytes.Length);
            //fileStream.Close();

            var stream = new MemoryStream(bytes);

            // act
            var terrainTile = TerrainTileParser.Parse(stream);
            var triangles = terrainTile.GetTriangles(0, 0, 0);

            // assert
            Assert.IsTrue(terrainTile != null);
            Assert.IsTrue(triangles.Count == 400);

            // check: coordinates are CCW order
            Assert.IsTrue(triangles[0].Coordinate1.X == -180);
            Assert.IsTrue(triangles[0].Coordinate1.Y == -78.755149998474081);
            Assert.IsTrue(triangles[0].Coordinate1.Height == -55.24706495350631);

            Assert.IsTrue(triangles[0].Coordinate2.X == -180);
            Assert.IsTrue(triangles[0].Coordinate2.Y == -90);
            Assert.IsTrue(triangles[0].Coordinate2.Height == -29.85938702932947);

            Assert.IsTrue(triangles[0].Coordinate3.X == -168.75514999847408);
            Assert.IsTrue(triangles[0].Coordinate3.Y == -81.56773583178199);
            Assert.IsTrue(triangles[0].Coordinate3.Height == -50.34768851199851);
        }


        [Test]
        public void TestAnotherTileParsing1()
        {
            // arrange
            const string firstTerrainFile = "Terrain.Tile.Tests.data.test.terrain";

            // act
            var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(firstTerrainFile);
            var terrainTile = TerrainTileParser.Parse(pbfStream);

            Assert.IsTrue(terrainTile != null);

            // todo: check extensions
        }

    }
}