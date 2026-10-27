using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Benchmark
{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net10_0)]
    [SimpleJob(RuntimeMoniker.NativeAot10_0)]
    [HideColumns("Error", "StdDev", "RatioSD")]
    public partial class CompareStringBuildersToString
    {
        [Params(100, 1000, 10_000, 100_000)]
        public int StrLength;

        private string _str;
        private System.Text.StringBuilder _sb;
        private StringBuilderArray.StringBuilderArray _sbArr;

        [IterationSetup]
        public void Setup()
        {
            _str = new string('S', StrLength);
            _sb = new System.Text.StringBuilder();
            for (int i = 0; i < 1000; i++)
            {
                _sb.AppendLine(_str);
            }

            _sbArr = new StringBuilderArray.StringBuilderArray();
            for (int i = 0; i < 1000; i++)
            {
                _sbArr.AppendLine(_str);
            }
        }

        [Benchmark(Baseline = true, Description = "StringBuilderArray")]
        public void StringBuilderArray()
        {
            _sbArr.ToString();
        }

        [Benchmark(Description = "StringBuilder")]
        public void StringBuilder()
        {
            _sb.ToString();
        }
    }
}