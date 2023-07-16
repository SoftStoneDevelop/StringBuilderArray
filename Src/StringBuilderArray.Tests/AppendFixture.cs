using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace StringBuilderArrayTests
{
    [TestFixture]
    public class AppendFixture
    {
        public static object[] LengthCases = { 5, 7, 10, 50, 100, 1000, 2500 };

        [Test]
        [TestCaseSource(nameof(LengthCases))]
        public void AppendTest(int length)
        {
            var strBuilded = new string('a', length - 1);
            var sb = new StringBuilder();
            for (int i = 0; i < 53; i++)
            {
                sb.Append(i.ToString() + strBuilded);
#if NET6_0_OR_GREATER
                sb.Append($"Interpool {i}");
#endif
            }

            var sbArr = new StringBuilderArray.StringBuilderArray();
            for (int i = 0; i < 53; i++)
            {
                sbArr.Append(i.ToString() + strBuilded);
#if NET6_0_OR_GREATER
                sbArr.Append($"Interpool {i}");
#endif
            }

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
            Assert.That(sbArr.ToString(), Is.EqualTo(sb.ToString()));
        }

        [Test]
        [TestCaseSource(nameof(LengthCases))]
        public void AppendClearTest(int length)
        {
            var strBuilded = new string('a', length - 1);
            var sb = new StringBuilder();
            for (int i = 53; i < 70; i++)
            {
                sb.Append(i.ToString() + strBuilded);
#if NET6_0_OR_GREATER
                sb.Append($"Interpool {i}");
#endif
            }

            var sbArr = new StringBuilderArray.StringBuilderArray();
            for (int i = 0; i < 53; i++)
            {
                sbArr.Append(i.ToString() + strBuilded);
#if NET6_0_OR_GREATER
                sbArr.Append($"Interpool {i}");
#endif
            }
            sbArr.Clear();
            for (int i = 53; i < 70; i++)
            {
                sbArr.Append(i.ToString() + strBuilded);
#if NET6_0_OR_GREATER
                sbArr.Append($"Interpool {i}");
#endif
            }

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
            Assert.That(sbArr.ToString(), Is.EqualTo(sb.ToString()));
        }

        [Test]
        [TestCaseSource(nameof(LengthCases))]
        public void AppendEnumerableTest(int length)
        {
            var strBuilded = new string('a', length - 1);
            var list = new List<string>();
            for (int i = 0; i < 53; i++)
            {
                var text = i.ToString() + strBuilded;
                list.Add(text);
#if NET6_0_OR_GREATER
                list.Add("Interpool ");
                list.Add(i.ToString());
#endif
            }

            var sbArr = new StringBuilderArray.StringBuilderArray();
            for (int i = 0; i < 53; i++)
            {
                sbArr.Append(i.ToString() + strBuilded);
#if NET6_0_OR_GREATER
                sbArr.Append($"Interpool {i}");
#endif
            }

            var index = 0;
            foreach (var str in sbArr)
            {
                Assert.That(str, Is.EqualTo(list[index++]));
            }
        }
    }
}