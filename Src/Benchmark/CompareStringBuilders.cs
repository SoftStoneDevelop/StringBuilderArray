using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Runtime.CompilerServices;

namespace Benchmark
{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net10_0)]
    [SimpleJob(RuntimeMoniker.NativeAot10_0)]
    [HideColumns("Error", "StdDev", "Median", "RatioSD", "Gen0", "Gen1", "Gen2")]
    public partial class CompareStringBuilders
    {
        [Params(5, 7, 10, 50, 100, 1000, 2500, 5_000, 10_000, 100_000, 500_000, 1_071_741)]
        public int StrLength;

        private string _str;

        [IterationSetup]
        public void Setup()
        {
            _str = new string('S', StrLength);
        }

        [Benchmark(Baseline = true, Description = "StringBuilderArray")]
        public void StringBuilderArray()
        {
            var sb = new StringBuilderArray.StringBuilderArray();
            for (int i = 0; i < 1000; i++)
            {
                sb.AppendLine(_str);
            }
        }

        [Benchmark(Description = "StringBuilder")]
        public void StringBuilder()
        {
            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < 1000; i++)
            {
                sb.AppendLine(_str);
            }
        }

        [Benchmark(Description = "DefaultInterpolatedStringHandler")]
        public void DefaultInterpolatedStringHandler()
        {
            var sb = new DefaultInterpolatedStringHandler();
            for (int i = 0; i < 1000; i++)
            {
                sb.AppendLiteral(_str);
            }
        }
    }
}