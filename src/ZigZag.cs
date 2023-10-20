namespace Terrain.Tiles;

public static class ZigZag
{
    public static int Decode(ushort n)
    {
        return (n >> 1) ^ (-(n & 1));
    }

    public static ushort Encode(long n)
    {
        return (ushort)((n >> 31) ^ (n << 1));
    }
}
