using BenchmarkDotNet.Running;
using System;

namespace quantized_mesh_tile_cs.benchmark;

class Program
{
    static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<ParsingBenchmark>();
        Console.ReadKey();
    }
}
