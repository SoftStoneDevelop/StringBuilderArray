using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Benchmark
{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net70)]
    [HideColumns("Error", "StdDev", "Median", "RatioSD", "Gen0", "Gen1", "Gen2")]
    public partial class CompareStringBuildersToString
    {
        [Params(5, 7, 10, 50, 100, 1000, 2500, 5_000, 10_000, 100_000, 500_000, 1_071_741)]
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

        [Benchmark(Baseline = true, Description = "StringBuilder")]
        public void StringBuilder()
        {
            _sb.ToString();
        }

        [Benchmark(Description = "StringBuilderArray")]
        public void StringBuilderArray()
        {
            _sbArr.ToString();
        }
    }
}