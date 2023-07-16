using NUnit.Framework;
using System.Collections.Generic;

namespace StringBuilderArrayTests
{
    internal class InsertFixture
    {
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

#if NET6_0_OR_GREATER
        [Test]
        public void InsertInterpool()
        {
            var literal = "top";
            var expect = new List<string>()
            {
                "Number ", "0",
                "Number ", "1",
                "Number ", "2",
                "Number ", "3",
                "Number ", "4",
                "Number ", "5",
                "Number ", "6",
                "Number ", "7",
                "Number ", "8",
                "Number ", "9"
            };//count 20

            var sbArr = new StringBuilderArray.StringBuilderArray();
            for (int i = 0; i < 10; i++)
            {
                sbArr.Append($"Number {i}");
            }

            expect.Insert(expect.Count - 2, "Secret before end ");
            expect.Insert(expect.Count - 2, literal);
            //count 22
            sbArr.Insert(1, $"Secret before end {literal}");

            var index = 0;
            foreach (var str in sbArr)
            {
                Assert.That(str, Is.EqualTo(expect[index++]));
            }

            expect.Insert(0, "Secret start ");
            expect.Insert(1, literal);
            //count 24
            sbArr.Insert(21, $"Secret start {literal}");

            index = 0;
            foreach (var str in sbArr)
            {
                Assert.That(str, Is.EqualTo(expect[index++]));
            }

            expect.Insert(2, "Secret index 1 ");
            expect.Insert(3, literal);
            //count 26
            sbArr.Insert(21, $"Secret index 1 {literal}");

            index = 0;
            foreach (var str in sbArr)
            {
                Assert.That(str, Is.EqualTo(expect[index++]));
            }

            expect.Insert(8, "Secret index 9 ");
            expect.Insert(9, literal);
            //count 28
            sbArr.Insert(17, $"Secret index 9 {literal}");

            index = 0;
            foreach (var str in sbArr)
            {
                Assert.That(str, Is.EqualTo(expect[index++]));
            }
        }
#endif
    }
}