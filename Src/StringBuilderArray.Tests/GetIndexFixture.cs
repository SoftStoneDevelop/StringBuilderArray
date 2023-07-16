using NUnit.Framework;
using System.Collections.Generic;

namespace StringBuilderArrayTests
{
    [TestFixture]
    public class GetIndexFixture
    {
        [Test]
        public void GetIndex()
        {
            var sbArr = new StringBuilderArray.StringBuilderArray();
            for (int i = 0; i < 10; i++)
            {
                sbArr.Append(i.ToString());
                Assert.That(sbArr.GetIndex(i.ToString()), Is.EqualTo(0));
            }

            var index = 0;
            for (int i = 10 - 1; i >= 0; i--)
            {
                Assert.That(sbArr.GetIndex(i.ToString()), Is.EqualTo(index++));
            }

            Assert.That(sbArr.GetIndex("10"), Is.EqualTo(-1));
        }

        [Test]
        public void Insert()
        {
            var expect = new List<string>()
            {
                "0", "1","2","3","4","5","6","7","8","9"
            };//count 10

            var sbArr = new StringBuilderArray.StringBuilderArray();
            for (int i = 0; i < 10; i++)
            {
                sbArr.Append(i.ToString());
            }

            expect.Insert(expect.Count - 1, "Secret before end");
            //count 11
            sbArr.Insert(0, "Secret before end");

            var index = 0;
            foreach (var str in sbArr)
            {
                Assert.That(str, Is.EqualTo(expect[index++]));
            }

            expect.Insert(0, "Secret start");
            //count 12
            sbArr.Insert(10, "Secret start");

            index = 0;
            foreach (var str in sbArr)
            {
                Assert.That(str, Is.EqualTo(expect[index++]));
            }

            expect.Insert(1, "Secret index 1");
            //count 13
            sbArr.Insert(10, "Secret index 1");

            index = 0;
            foreach (var str in sbArr)
            {
                Assert.That(str, Is.EqualTo(expect[index++]));
            }

            expect.Insert(9, "Secret index 9");
            //count 14
            sbArr.Insert(3, "Secret index 9");

            index = 0;
            foreach (var str in sbArr)
            {
                Assert.That(str, Is.EqualTo(expect[index++]));
            }
        }
    }
}