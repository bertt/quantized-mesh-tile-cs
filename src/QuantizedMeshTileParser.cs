using System.IO;

namespace Quantized.Mesh.Tile
{
    public class QuantizedMeshTileParser
    {
        public static QuantizedMeshTile Parse(Stream tileStream)
        {
            var quantizedMeshTile = new QuantizedMeshTile();
            //using (var zStream = new GZipStream(tileStream, CompressionMode.Decompress))
            //{
            using (var reader = new FastBinaryReader(tileStream))
            {
                quantizedMeshTile.Header = new QuantizedMeshHeader(reader);
                quantizedMeshTile.VertexData = new VertexData(reader);
                quantizedMeshTile.IndexData16 = new IndexData16(reader);
                quantizedMeshTile.EdgeIndices16 = new EdgeIndices16(reader);

                // NormalExtensionData normalData;
                while (reader.HasMore())
                {
                    var extensionHeader = new ExtensionHeader(reader);

                    if (extensionHeader.extensionId == 1)
                    {
                        // oct-encoded per vertex normals
                        // todo:
                        quantizedMeshTile.NormalExtensionData = new NormalExtensionData(reader, quantizedMeshTile.VertexData.vertexCount);
                    }
                }

                // ?? int a = 10;
            }
            //}
            return quantizedMeshTile;
        }
    }
}
