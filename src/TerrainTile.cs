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
}
