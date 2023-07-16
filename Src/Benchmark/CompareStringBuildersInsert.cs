using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Benchmark
{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net70)]
    [HideColumns("Error", "StdDev", "Median", "RatioSD", "Gen0", "Gen1", "Gen2")]
    public partial class CompareStringBuildersInsert
    {
        [Params(5, 7, 10, 50, 100, 1000, 2500, 5_000, 10_000, 100_000, 500_000, 1_071_741)]
        public int StrLength;

        private string _str;

        [IterationSetup]
        public void Setup()
        {
            _str = new string('S', StrLength);
        }

        [Benchmark(Baseline = true, Description = "StringBuilder")]
        public void StringBuilder()
        {
            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < 100; i++)
            {
                sb.Append(_str);
            }

            //before 1 str from end
            sb.Insert(99 * _str.Length, _str);

            //in start
            sb.Insert(0, _str);

            //after 5 str from start
            sb.Insert(5 * _str.Length, _str);
        }

        [Benchmark(Description = "StringBuilderArray")]
        public void StringBuilderArray()
        {
            var sb = new StringBuilderArray.StringBuilderArray();
            for (int i = 0; i < 100; i++)
            {
                sb.Append(_str);
            }

            sb.Insert(0, _str);

            //in start
            sb.Insert(100, _str);

            //after 5 str from start
            sb.Insert(96, _str);
        }
    }
}