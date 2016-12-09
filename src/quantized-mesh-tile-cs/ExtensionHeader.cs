using System.Runtime.InteropServices;

namespace Quantized.Mesh.Tile
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ExtensionHeader
    {
        public byte extensionId;
        public uint extensionLength;

        public ExtensionHeader(FastBinaryReader reader)
        {
            extensionId = reader.ReadByte();
            extensionLength = reader.ReadUInt32();
        }
    }
}
