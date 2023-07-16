using BenchmarkDotNet.Running;

namespace Benchmark
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<CompareStringBuildersToString>();
            BenchmarkRunner.Run<CompareStringBuilders>();
            BenchmarkRunner.Run<CompareStringBuildersClear>();
            BenchmarkRunner.Run<CompareStringBuildersInsert>();
        }
    }
}