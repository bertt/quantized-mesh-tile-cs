using BenchmarkDotNet.Attributes;
using System.IO;
using Terrain.Tiles;

namespace quantized_mesh_tile_cs.benchmark
{
    public class ParsingBenchmark
    {
        [Benchmark]
        public TerrainTile ParseVectorTileFromStream()
        {
            var stream = File.OpenRead(@"data/9_533_383.terrain");
            return TerrainTileParser.Parse(stream);
        }
    }
}
