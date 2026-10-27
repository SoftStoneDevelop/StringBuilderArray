using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Benchmark
{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net10_0)]
    [SimpleJob(RuntimeMoniker.NativeAot10_0)]
    public partial class CompareStringBuildersInsert
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
        public void MineStringBuilderArray()
        {
            var sb = new StringBuilderArray.StringBuilderArray();
            for (int i = 0; i < 100; i++)
            {
                sb.Append(_str);
            }

            sb.InsertByIndex(99 * _str.Length, _str, StringBuilderArray.IndexDirection.FromStart);

            //in start
            sb.InsertByIndex(0, _str, StringBuilderArray.IndexDirection.FromStart);

            //after 5 str from start
            sb.InsertByIndex(5 * _str.Length, _str, StringBuilderArray.IndexDirection.FromStart);
        }

        [Benchmark(Description = "StringBuilder")]
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
    }
}