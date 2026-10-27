using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace StringBuilderArrayTests
{
    [TestFixture]
    public class AppendArrayFixture
    {
        public static readonly object[] LengthCases = { 5, 7, 10, 50, 100, 1000, 2500 };

        [Test]
        [TestCaseSource(nameof(LengthCases))]
        public void AppendArrayTest(int length)
        {
            var strBuilded = new string('a', length - 1);
            var sb = new StringBuilder();
            var list = new List<string>();
            for (int i = 0; i < 53; i++)
            {
                var text = i.ToString() + strBuilded;
                list.Add(text);
                sb.Append(text);
            }

            var sbArr = new StringBuilderArray.StringBuilderArray();
            sbArr.Append(list);

            Assert.That(sb.ToString(), Is.EqualTo(sbArr.ToString()));
#if NET5_0_OR_GREATER
            var expectSpan = sb.ToString().AsSpan();
            var resultSpan = GC.AllocateUninitializedArray<char>(sbArr.Length);
            sbArr.ToString(resultSpan);
            for (int i = 0; i < expectSpan.Length; i++)
            {
                var expectChar = expectSpan[i];
                var haveChar = resultSpan[i];
                Assert.That(haveChar, Is.EqualTo(expectChar));
            }
#endif
            var index = 0;
            foreach (var str in sbArr)
            {
                Assert.That(str, Is.EqualTo(sb[index++]));
            }
        }

        [Test]
        [TestCaseSource(nameof(LengthCases))]
        public void AppendLineArrayTest(int length)
        {
            var strBuilded = new string('a', length - 1);
            var sb = new StringBuilder();
            var lines = new List<string>();
            for (int i = 0; i < 53; i++)
            {
                var text = i.ToString() + strBuilded;
                lines.Add(text);
                sb.AppendLine(text);
            }

            var sbArr = new StringBuilderArray.StringBuilderArray();
            sbArr.AppendLine(lines);

            Assert.That(sbArr.ToString(), Is.EqualTo(sb.ToString()));
#if NET5_0_OR_GREATER
            var expectSpan = sb.ToString().AsSpan();
            var resultSpan = GC.AllocateUninitializedArray<char>(sbArr.Length);
            sbArr.ToString(resultSpan);
            for (int i = 0; i < expectSpan.Length; i++)
            {
                var expectChar = expectSpan[i];
                var haveChar = resultSpan[i];
                Assert.That(haveChar, Is.EqualTo(expectChar));
            }
#endif

            var index = 0;
            foreach (var str in sbArr)
            {
                Assert.That(str, Is.EqualTo(sb[index++]));
            }
        }
    }
}