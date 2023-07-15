using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace StringBuilderArrayTests
{
    [TestFixture]
    public class StringBuilderArrayFixture
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
            }

            var sbArr = new StringBuilderArray.StringBuilderArray();
            for (int i = 0; i < 53; i++)
            {
                sbArr.Append(i.ToString() + strBuilded);
            }

            Assert.That(sb.ToString(), Is.EqualTo(sbArr.ToString()));
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
            }

            var sbArr = new StringBuilderArray.StringBuilderArray();
            for (int i = 0; i < 53; i++)
            {
                sbArr.Append(i.ToString() + strBuilded);
            }
            sbArr.Clear();
            for (int i = 53; i < 70; i++)
            {
                sbArr.Append(i.ToString() + strBuilded);
            }

            Assert.That(sb.ToString(), Is.EqualTo(sbArr.ToString()));
        }

        [Test]
        [TestCaseSource(nameof(LengthCases))]
        public void AppendLineTest(int length)
        {
            var strBuilded = new string('a', length - 1);
            var sb = new StringBuilder();
            for (int i = 0; i < 53; i++)
            {
                sb.AppendLine(i.ToString() + strBuilded);
            }

            var sbArr = new StringBuilderArray.StringBuilderArray();
            for (int i = 0; i < 53; i++)
            {
                sbArr.AppendLine(i.ToString() + strBuilded);
            }

            Assert.That(sb.ToString(), Is.EqualTo(sbArr.ToString()));
        }

        [Test]
        [TestCaseSource(nameof(LengthCases))]
        public void AppendLineClearTest(int length)
        {
            var strBuilded = new string('a', length - 1);
            var sb = new StringBuilder();
            for (int i = 53; i < 70; i++)
            {
                sb.AppendLine(i.ToString() + strBuilded);
            }

            var sbArr = new StringBuilderArray.StringBuilderArray();
            for (int i = 0; i < 53; i++)
            {
                sbArr.AppendLine(i.ToString() + strBuilded);
            }
            sbArr.Clear();
            for (int i = 53; i < 70; i++)
            {
                sbArr.AppendLine(i.ToString() + strBuilded);
            }

            Assert.That(sb.ToString(), Is.EqualTo(sbArr.ToString()));
        }

        [Test]
        [TestCaseSource(nameof(LengthCases))]
        public void AppendLineEnumerableTest(int length)
        {
            var strBuilded = new string('a', length - 1);
            var list = new List<string>();
            for (int i = 0; i < 53; i++)
            {
                var text = i.ToString() + strBuilded;
                list.Add(text);
                list.Add(Environment.NewLine);
            }

            var sbArr = new StringBuilderArray.StringBuilderArray();
            for (int i = 0; i < 53; i++)
            {
                sbArr.AppendLine(i.ToString() + strBuilded);
            }

            var index = 0;
            foreach (var str in sbArr)
            {
                Assert.That(list[index++], Is.EqualTo(str));
            }
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
            }

            var sbArr = new StringBuilderArray.StringBuilderArray();
            for (int i = 0; i < 53; i++)
            {
                sbArr.Append(i.ToString() + strBuilded);
            }

            var index = 0;
            foreach (var str in sbArr)
            {
                Assert.That(list[index++], Is.EqualTo(str));
            }
        }

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

            var index = 0;
            foreach (var str in sbArr)
            {
                Assert.That(list[index++], Is.EqualTo(str));
            }
        }

        [Test]
        [TestCaseSource(nameof(LengthCases))]
        public void AppendLineArrayTest(int length)
        {
            var strBuilded = new string('a', length - 1);
            var sb = new StringBuilder();
            var list = new List<string>();
            var listInput = new List<string>();
            for (int i = 0; i < 53; i++)
            {
                var text = i.ToString() + strBuilded;
                listInput.Add(text);
                list.Add(text);
                list.Add(Environment.NewLine);
                sb.AppendLine(text);
            }

            var sbArr = new StringBuilderArray.StringBuilderArray();
            sbArr.AppendLine(listInput);

            Assert.That(sb.ToString(), Is.EqualTo(sbArr.ToString()));

            var index = 0;
            foreach (var str in sbArr)
            {
                Assert.That(list[index++], Is.EqualTo(str));
            }
        }
    }
}