using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Runtime.CompilerServices;

namespace Benchmark
{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net10_0)]
    [SimpleJob(RuntimeMoniker.NativeAot10_0)]
    [HideColumns("Error", "StdDev", "RatioSD")]
    public partial class CompareStringBuildersClear
    {
        [Params(100, 1000, 10_000, 100_000)]
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

            sb.Clear();
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

            sb.Clear();
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

            sb.Clear();
            for (int i = 0; i < 1000; i++)
            {
                sb.AppendLiteral(_str);
            }
        }
    }
}