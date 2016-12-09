using System.IO;
using System.IO.Compression;

namespace Quantized.Mesh.Tile
{
    public class QuantizedMeshTileParser
    {
        public static void Parse(Stream tileStream)
        {
            using (var zStream = new GZipStream(tileStream, CompressionMode.Decompress))
            {
                using (var reader = new FastBinaryReader(zStream))
                {
                    // read header
                    var header = new QuantizedMeshHeader(reader);
                    var vertexData = new VertexData(reader);
                    var indexData = new IndexData16(reader);
                    var edgeIndexData = new EdgeIndices16(reader);

                    NormalExtensionData normalData;
                    while (reader.HasMore())
                    {
                        var extensionHeader = new ExtensionHeader(reader);

                        if (extensionHeader.extensionId == 1)
                        {
                            // oct-encoded per vertex normals
                            normalData = new NormalExtensionData(reader, vertexData.vertexCount);
                        }
                    }

                    // ?? int a = 10;
                }
            }
        }

    }
}
