using NUnit.Framework;
using System;

namespace StringBuilderArrayTests
{
    [TestFixture]
    public class ToStringFixture
    {
        [Test]
        public void ToStringTest()
        {
            var sbArr = new StringBuilderArray.StringBuilderArray();
            for (int i = 0; i < 10; i++)
            {
                sbArr.Append(i.ToString());
                Assert.That(sbArr.GetIndex(i.ToString()), Is.EqualTo(0));
            }

            var str = sbArr.ToString();
            Assert.That(str, Is.EqualTo("0123456789"));
        }

#if NET6_0_OR_GREATER
        [Test]
        public void ToStringInBuffer()
        {
            var sbArr = new StringBuilderArray.StringBuilderArray();
            for (int i = 0; i < 10; i++)
            {
                sbArr.Append(i.ToString());
            }

            char[] buffer = new char[sbArr.Length];
            sbArr.ToString(buffer.AsSpan());
            Assert.That(buffer, Is.EqualTo("0123456789"));

            char[] bufferBigger = new char[sbArr.Length + 30];
            sbArr.ToString(bufferBigger.AsSpan());
            Assert.That(bufferBigger.AsSpan(0, sbArr.Length).ToArray(), Is.EqualTo("0123456789"));

            char[] bufferShorter = new char[sbArr.Length - 1];
            Assert.Throws<ArgumentException>(() => { sbArr.ToString(bufferShorter.AsSpan()); });
        }
#endif
    }
}