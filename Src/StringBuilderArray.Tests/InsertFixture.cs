using NUnit.Framework;
using System.Collections.Generic;

namespace StringBuilderArrayTests
{
    internal class InsertFixture
    {
        private static IEnumerable<TestCaseData> AddCases()
        {
            yield return new TestCaseData(9, "BeforeEnd").SetName("Before end");
            yield return new TestCaseData(10, "End").SetName("In the end");
            yield return new TestCaseData(5, "InMiddle").SetName("In the middle");
            yield return new TestCaseData(0, "Start").SetName("On start");
            yield return new TestCaseData(1, "Second").SetName("Second");
        }

        [TestCaseSource(nameof(AddCases))]
        public void Insert(int insertIndexFromStart, string insertValue)
        {
            var expectStart = "0123456789";
            var sbFromEnd = new StringBuilderArray.StringBuilderArray(10);
            var sbFromStart = new StringBuilderArray.StringBuilderArray(10);
            for (int i = 0; i < 10; i++)
            {
                sbFromEnd.Append(i.ToString());
                sbFromStart.Append(i.ToString());
            }

            var expectedResult = expectStart.Insert(insertIndexFromStart, insertValue);
            sbFromStart.InsertByIndex(insertIndexFromStart, insertValue, StringBuilderArray.IndexDirection.FromStart);
            var indexFromEnd = expectStart.Length - insertIndexFromStart;
            sbFromEnd.InsertByIndex(indexFromEnd, insertValue, StringBuilderArray.IndexDirection.FromEnd);

            Assert.That(sbFromStart.ToString(), Is.EqualTo(expectedResult), "Error in FromStart");
            Assert.That(sbFromEnd.ToString(), Is.EqualTo(expectedResult), "Error in FromEnd");
        }

        [Test]
        public void InsertIntoOneString()
        {
            var sbFromStart = new StringBuilderArray.StringBuilderArray();
            var sbFromEnd = new StringBuilderArray.StringBuilderArray();

            sbFromStart.Append("ABCDEF");
            sbFromEnd.Append("ABCDEF");

            // "ABC-INSERT-DEF"
            sbFromStart.InsertByIndex(3, "INSERT", StringBuilderArray.IndexDirection.FromStart);
            sbFromEnd.InsertByIndex(3, "INSERT", StringBuilderArray.IndexDirection.FromEnd);

            var expect = "ABCINSERTDEF";
            Assert.That(sbFromStart.ToString(), Is.EqualTo(expect), "Error in FromStart");
            Assert.That(sbFromEnd.ToString(), Is.EqualTo(expect), "Error in FromEnd");
        }

        [Test]
        public void InsertIntoPreviousChunk()
        {
            var sbFromStart = new StringBuilderArray.StringBuilderArray(10);
            var sbFromEnd = new StringBuilderArray.StringBuilderArray(10);
            string expect = "START_";
            for (int i = 0; i < 15; i++)
            {
                expect += $"[{i}]";
                sbFromStart.Append($"[{i}]");
                sbFromEnd.Append($"[{i}]");
            }

            sbFromStart.InsertByIndex(0, "START_", StringBuilderArray.IndexDirection.FromStart);
            sbFromEnd.InsertByIndex(expect.Length - "START_".Length, "START_", StringBuilderArray.IndexDirection.FromEnd);
            Assert.That(sbFromStart.ToString(), Is.EqualTo(expect));
            Assert.That(sbFromEnd.ToString(), Is.EqualTo(expect));
        }
    }
}