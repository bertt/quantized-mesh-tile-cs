using BenchmarkDotNet.Attributes;
using System.IO;
using System.Reflection;
using Terrain.Tiles;

namespace quantized_mesh_tile_cs.benchmark
{
    public class ParsingBenchmark
    {
        Stream input;

        public ParsingBenchmark()
        {
            const string firstTerrainFile = "benchmark.data.9_533_383.terrain";
            input = Assembly.GetExecutingAssembly().GetManifestResourceStream(firstTerrainFile);
        }

        [Benchmark]
        public TerrainTile ParseVectorTileFromStream()
        {
            return TerrainTileParser.Parse(input);
        }
    }
}
