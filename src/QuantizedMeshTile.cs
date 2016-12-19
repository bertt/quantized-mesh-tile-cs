namespace Quantized.Mesh.Tile
{
    public class QuantizedMeshTile
    {
        public QuantizedMeshHeader Header {get;set;}
        public VertexData VertexData { get; set; }
        public IndexData16 IndexData16 { get; set; }
        public EdgeIndices16 EdgeIndices16 { get; set; }
        public NormalExtensionData NormalExtensionData { get; set; }
    }
}
