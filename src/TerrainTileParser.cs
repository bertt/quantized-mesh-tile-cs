using System;
using System.IO;

namespace Terrain.Tiles;

public class TerrainTileParser
{
    public static TerrainTile Parse(Stream tileStream)
    {
        var terrainTile = new TerrainTile();

        using (var reader = new BinaryReader(tileStream))
        {
            terrainTile.Header = new TerrainTileHeader(reader);
            terrainTile.VertexData = new VertexData(reader);
            terrainTile.IndexData16 = new IndexData16(reader);
            terrainTile.EdgeIndices16 = new EdgeIndices16(reader);

            // do not read extentions right now...
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                var extensionHeader = new ExtensionHeader(reader);

                // extensionid 1: per vertex lighting attributes
                // https://github.com/CesiumGS/quantized-mesh#terrain-lighting
                if (extensionHeader.extensionId == 1)
                {
                    var normals = reader.ReadBytes((int)extensionHeader.extensionLength);
                    terrainTile.HasNormals  = true;

                    // oct-encoded per vertex normals
                    // todo:
                    // quantizedMeshTile.NormalExtensionData = new NormalExtensionData(reader, quantizedMeshTile.VertexData.vertexCount);
                }
                else if (extensionHeader.extensionId == 2)
                {
                    terrainTile.HasWatermask = true;
                    var watermask = reader.ReadBytes((int)extensionHeader.extensionLength);
                    if (watermask.Length == 1)
                    { 
                        // 0=land, 255=water
                        var w = Convert.ToInt32(watermask[0]);
                    }
                    else 
                    { 
                        // grid of 256 * 256 with values 0 - 255
                        // but is it row-first or colum-first?
                    }
                }
                else if(extensionHeader.extensionId == 4)
                {
                    terrainTile.HasMetadata = true;
                    var length = reader.ReadUInt32();
                    var json = System.Text.Encoding.UTF8.GetString(reader.ReadBytes((int)length));
                    // todo extensionid 4: Metadata
                }
            }
        }
        return terrainTile;
    }
}
