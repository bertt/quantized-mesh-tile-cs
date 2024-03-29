﻿using NUnit.Framework;

namespace Terrain.Tiles.Tests;

public class TerrainTileParserTests
{
    [Test]
    public void TestTerrainTileWithNormalsAndMetadataParsing()
    {
        // arrange
        // Terrain tile from cesium ion
        const string firstTerrainFile = "data/normals_metadata.terrain";

        // act
        var pbfStream = File.OpenRead(firstTerrainFile);
        var terrainTile = TerrainTileParser.Parse(pbfStream);
    }


    [Test]
    public void TestTomTileParsing()
    {
        // arrange
        const string firstTerrainFile = "data/86.terrain";

        // act
        var pbfStream = File.OpenRead(firstTerrainFile);
        var terrainTile = TerrainTileParser.Parse(pbfStream);

        // assert
        Assert.IsTrue(terrainTile != null);
        Assert.IsTrue(terrainTile.VertexData.vertexCount> 0);
    }

    [Test]
    public void TestFirstTileParsing()
    {
        // arrange
        const string firstTerrainFile = "data/9_533_383.terrain";

        // act
        var pbfStream = File.OpenRead(firstTerrainFile);
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
    public void TestWatermarkTileParsing()
    {
        // arrange
        const string firstTerrainFile = "data/9_769_319_watermask.terrain";

        // act
        var pbfStream = File.OpenRead(firstTerrainFile);
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
        const string firstTerrainFile = "data/10_1563_590_light_watermask.terrain";

        // act
        var pbfStream = File.OpenRead(firstTerrainFile);
        var terrainTile = TerrainTileParser.Parse(pbfStream);

        Assert.IsTrue(terrainTile != null);

        // todo: check extensions
    }

    [Test]
    public void TestAnotherTileParsing1()
    {
        // arrange
        const string firstTerrainFile = "data/test_0_0_0.terrain";

        // act
        var pbfStream = File.OpenRead(firstTerrainFile);
        var terrainTile = TerrainTileParser.Parse(pbfStream);

        Assert.IsTrue(terrainTile != null);

        // todo: check extensions
    }
}