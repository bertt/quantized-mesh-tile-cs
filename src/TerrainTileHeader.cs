using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace Terrain.Tiles;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct TerrainTileHeader
{
    // The center of the tile in Earth-centered Fixed coordinates.
    public double CenterX;
    public double CenterY;
    public double CenterZ;

    // The minimum and maximum heights in the area covered by this tile.
    // The minimum may be lower and the maximum may be higher than
    // the height of any vertex in this tile in the case that the min/max vertex
    // was removed during mesh simplification, but these are the appropriate
    // values to use for analysis or visualization.
    public float MinimumHeight;
    public float MaximumHeight;

    // The tile’s bounding sphere.  The X,Y,Z coordinates are again expressed
    // in Earth-centered Fixed coordinates, and the radius is in meters.
    public double BoundingSphereCenterX;
    public double BoundingSphereCenterY;
    public double BoundingSphereCenterZ;
    public double BoundingSphereRadius;

    // The horizon occlusion point, expressed in the ellipsoid-scaled Earth-centered Fixed frame.
    // If this point is below the horizon, the entire tile is below the horizon.
    // See http://cesiumjs.org/2013/04/25/Horizon-culling/ for more information.
    public double HorizonOcclusionPointX;
    public double HorizonOcclusionPointY;
    public double HorizonOcclusionPointZ;

    public TerrainTileHeader(BinaryReader reader)
    {
        CenterX = reader.ReadDouble();
        CenterY = reader.ReadDouble();
        CenterZ = reader.ReadDouble();

        MinimumHeight = reader.ReadSingle();
        MaximumHeight = reader.ReadSingle();

        BoundingSphereCenterX = reader.ReadDouble();
        BoundingSphereCenterY = reader.ReadDouble();
        BoundingSphereCenterZ = reader.ReadDouble();
        BoundingSphereRadius = reader.ReadDouble();

        HorizonOcclusionPointX = reader.ReadDouble();
        HorizonOcclusionPointY = reader.ReadDouble();
        HorizonOcclusionPointZ = reader.ReadDouble();
    }

    public byte[] AsBinary()
    {
        var centerXBytes = BitConverter.GetBytes(CenterX);
        var centerYBytes = BitConverter.GetBytes(CenterY);
        var centerZBytes = BitConverter.GetBytes(CenterZ);

        var minimumHeightBytes = BitConverter.GetBytes(MinimumHeight);
        var maximumHeightBytes = BitConverter.GetBytes(MaximumHeight);

        var boundingSphereCenterXBytes = BitConverter.GetBytes(BoundingSphereCenterX);
        var boundingSphereCenterYBytes = BitConverter.GetBytes(BoundingSphereCenterY);
        var boundingSphereCenterZBytes = BitConverter.GetBytes(BoundingSphereCenterZ);
        var boundingSphereRadiusBytes = BitConverter.GetBytes(BoundingSphereRadius);

        var horizonOcclusionPointXBytes = BitConverter.GetBytes(HorizonOcclusionPointX);
        var horizonOcclusionPointYBytes = BitConverter.GetBytes(HorizonOcclusionPointY);
        var horizonOcclusionPointZBytes = BitConverter.GetBytes(HorizonOcclusionPointZ);

        var bytes = new List<byte>();
        bytes.AddRange(centerXBytes);
        bytes.AddRange(centerYBytes);
        bytes.AddRange(centerZBytes);
            
        bytes.AddRange(minimumHeightBytes);
        bytes.AddRange(maximumHeightBytes);
            
        bytes.AddRange(boundingSphereCenterXBytes);
            
        bytes.AddRange(boundingSphereCenterYBytes);
        bytes.AddRange(boundingSphereCenterZBytes);
        bytes.AddRange(boundingSphereRadiusBytes);

        bytes.AddRange(horizonOcclusionPointXBytes);
        bytes.AddRange(horizonOcclusionPointYBytes);
        bytes.AddRange(horizonOcclusionPointZBytes);

        return bytes.ToArray();
    }
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var other = (TerrainTileHeader)obj;
        return Math.Abs(CenterX - other.CenterX) < 0.0001 &&
            Math.Abs(CenterY - other.CenterY) < 0.0001 &&
            Math.Abs(CenterZ - other.CenterZ) < 0.0001 &&
            Math.Abs(MinimumHeight - other.MinimumHeight) < 0.0001 &&
            Math.Abs(MaximumHeight - other.MaximumHeight) < 0.0001 &&
            Math.Abs(BoundingSphereCenterX - other.BoundingSphereCenterX) < 0.0001 &&
            Math.Abs(BoundingSphereCenterY - other.BoundingSphereCenterY) < 0.0001 &&
            Math.Abs(BoundingSphereCenterZ - other.BoundingSphereCenterZ) < 0.0001 &&
            Math.Abs(BoundingSphereRadius - other.BoundingSphereRadius) < 0.0001 &&
            Math.Abs(HorizonOcclusionPointX - other.HorizonOcclusionPointX) < 0.0001 &&
            Math.Abs(HorizonOcclusionPointY - other.HorizonOcclusionPointY) < 0.0001 &&
            Math.Abs(HorizonOcclusionPointZ - other.HorizonOcclusionPointZ) < 0.0001;
    }
}
