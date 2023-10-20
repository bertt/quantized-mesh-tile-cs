using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Terrain.Tiles;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe struct VertexData
{
    public uint vertexCount;
    public int[] u;
    public int[] v;
    public int[] height;

    public VertexData(BinaryReader reader)
    {
        vertexCount = reader.ReadUInt32();
        var _u = new ushort[vertexCount];
        var _v = new ushort[vertexCount];
        var _height = new ushort[vertexCount];

        if (vertexCount > 64 * 1024)
            throw new NotSupportedException("32 bit indices not supported yet");

        for (int i = 0; i < vertexCount; i++)
            _u[i] = reader.ReadUInt16();

        for (int i = 0; i < vertexCount; i++)
            _v[i] = reader.ReadUInt16();

        for (int i = 0; i < vertexCount; i++)
            _height[i] = reader.ReadUInt16();

        u = Decode(_u);
        v = Decode(_v);
        height = Decode(_height);
    }

    public static int[] Decode(ushort[] items)
    {
        var result = new int[items.Length];
        int item = 0;
        
        for (int i = 0; i < items.Length; i++)
        {
            var decode = ZigZag.Decode(items[i]);
            item += decode;

            result[i] = item;
        }
        return result;
    }

    public byte[] AsBinary()
    {
        var stream = new MemoryStream();
        var writer = new BinaryWriter(stream);

        var vertexCountBytes = BitConverter.GetBytes(vertexCount);
        writer.Write(vertexCountBytes);

        var u_encoded = Encode(u);
        var u_bytes = AsBytes(u_encoded);
        writer.Write(u_bytes);

        var v_encoded = Encode(v);
        var v_bytes = AsBytes(v_encoded);
        writer.Write(v_bytes);

        var h_encoded = Encode(height);
        var h_bytes = AsBytes(h_encoded);
        writer.Write(h_bytes);

        return stream.ToArray();
    }

    public static ushort[] Encode(int[] items)
    {
        var result = new ushort[items.Length];
        result[0] = ZigZag.Encode(items[0]);
        for (int i = 1; i < items.Length ; i++)
        {
            var ud = items[i] - items[i-1];
            var encode = ZigZag.Encode(ud);

            result[i] = encode;
        }
        return result;
    }
    private byte[] AsBytes(ushort[] items)
    {
        var stream = new MemoryStream();
        var writer = new BinaryWriter(stream);
        for (int i = 0; i < items.Length; i++)
        {
            var bytes = BitConverter.GetBytes(items[i]);
            writer.Write(bytes);
        }

        return stream.ToArray();
    }

}
