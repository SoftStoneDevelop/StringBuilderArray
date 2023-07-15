using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace StringBuilderArrayTests
{
    [TestFixture]
    public class StringBuilderArrayFixture
    {
        [Test]
        public void AppendTest()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < 53; i++)
            {
                sb.Append(i.ToString());
            }

            var sbArr = new StringBuilderArray.StringBuilderArray();
            for (int i = 0; i < 53; i++)
            {
                sbArr.Append(i.ToString());
            }

            Assert.That(sb.ToString(), Is.EqualTo(sbArr.ToString()));
        }

        [Test]
        public void AppendClearTest()
        {
            var sb = new StringBuilder();
            for (int i = 53; i < 70; i++)
            {
                sb.Append(i.ToString());
            }

            var sbArr = new StringBuilderArray.StringBuilderArray();
            for (int i = 0; i < 53; i++)
            {
                sbArr.Append(i.ToString());
            }
            sbArr.Clear();
            for (int i = 53; i < 70; i++)
            {
                sbArr.Append(i.ToString());
            }

            Assert.That(sb.ToString(), Is.EqualTo(sbArr.ToString()));
        }

        [Test]
        public void AppendLineTest()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < 53; i++)
            {
                sb.AppendLine(i.ToString());
            }

            var sbArr = new StringBuilderArray.StringBuilderArray();
            for (int i = 0; i < 53; i++)
            {
                sbArr.AppendLine(i.ToString());
            }

            Assert.That(sb.ToString(), Is.EqualTo(sbArr.ToString()));
        }

        [Test]
        public void AppendLineClearTest()
        {
            var sb = new StringBuilder();
            for (int i = 53; i < 70; i++)
            {
                sb.AppendLine(i.ToString());
            }

            var sbArr = new StringBuilderArray.StringBuilderArray();
            for (int i = 0; i < 53; i++)
            {
                sbArr.AppendLine(i.ToString());
            }
            sbArr.Clear();
            for (int i = 53; i < 70; i++)
            {
                sbArr.AppendLine(i.ToString());
            }

            Assert.That(sb.ToString(), Is.EqualTo(sbArr.ToString()));
        }

        [Test]
        public void AppendLineEnumerableTest()
        {
            var list = new List<string>();
            for (int i = 0; i < 53; i++)
            {
                var text = i.ToString();
                list.Add(text);
                list.Add(Environment.NewLine);
            }

            var sbArr = new StringBuilderArray.StringBuilderArray();
            for (int i = 0; i < 53; i++)
            {
                sbArr.AppendLine(i.ToString());
            }

            var index = 0;
            foreach (var str in sbArr)
            {
                Assert.That(list[index++], Is.EqualTo(str));
            }
        }

        [Test]
        public void AppendEnumerableTest()
        {
            var list = new List<string>();
            for (int i = 0; i < 53; i++)
            {
                var text = i.ToString();
                list.Add(text);
            }

            var sbArr = new StringBuilderArray.StringBuilderArray();
            for (int i = 0; i < 53; i++)
            {
                sbArr.Append(i.ToString());
            }

            var index = 0;
            foreach (var str in sbArr)
            {
                Assert.That(list[index++], Is.EqualTo(str));
            }
        }

        [Test]
        public void AppendArrayTest()
        {
            var sb = new StringBuilder();
            var list = new List<string>();
            for (int i = 0; i < 53; i++)
            {
                var text = i.ToString();
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
        public void AppendLineArrayTest()
        {
            var sb = new StringBuilder();
            var list = new List<string>();
            var listInput = new List<string>();
            for (int i = 0; i < 53; i++)
            {
                var text = i.ToString();
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