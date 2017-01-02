using System.IO;

namespace Terrain.Tiles
{
    public class TerrainTileParser
    {
        public static TerrainTile Parse(Stream tileStream)
        {
            var terrainTile = new TerrainTile();

            using (var reader = new FastBinaryReader(tileStream))
            {
                terrainTile.Header = new TerrainTileHeader(reader);
                terrainTile.VertexData = new VertexData(reader);
                terrainTile.IndexData16 = new IndexData16(reader);
                terrainTile.EdgeIndices16 = new EdgeIndices16(reader);

                // NormalExtensionData normalData;
                while (reader.HasMore())
                {
                    var extensionHeader = new ExtensionHeader(reader);

                    // extensionid 1: per vertex lighting attributes
                    if (extensionHeader.extensionId == 1)
                    {
                        // oct-encoded per vertex normals
                        // todo:
                        // quantizedMeshTile.NormalExtensionData = new NormalExtensionData(reader, quantizedMeshTile.VertexData.vertexCount);
                    }
                    else if (extensionHeader.extensionId == 2)
                    {
                        // todo extensionid 2: per vertex watermark
                    }
                }
            }
            return terrainTile;
        }
    }
}
