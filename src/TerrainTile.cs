using System.IO;

namespace Terrain.Tiles;

public class TerrainTile
{
    public TerrainTileHeader Header { get; set; }
    public VertexData VertexData { get; set; }
    public IndexData16 IndexData16 { get; set; }
    public EdgeIndices16 EdgeIndices16 { get; set; }

    public bool HasNormals { get; set; }
    public bool HasMetadata { get; set; }
    public bool HasWatermask { get; set; }

    public byte[] AsBinary()
    {
        using (var stream = new MemoryStream())
        {
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write(Header.AsBinary());
                writer.Write(VertexData.AsBinary());
                // todo add indexes and edgeindices
            }
            return stream.ToArray();
        }
    }
}
