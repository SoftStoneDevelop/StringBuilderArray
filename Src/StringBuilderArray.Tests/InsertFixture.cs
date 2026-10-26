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
        }

        [TestCaseSource(nameof(AddCases))]
        public void Insert(int insertIndexFromStart, string insertValue)
        {
            var expectStart = "0123456789";
            var sbFromEnd = new StringBuilderArray.StringBuilderArray();
            var sbFromStart = new StringBuilderArray.StringBuilderArray();
            for (int i = 0; i < 10; i++)
            {
                sbFromEnd.Append(i.ToString());
                sbFromStart.Append(i.ToString());
            }

            var expectedResult = expectStart.Insert(insertIndexFromStart, insertValue);
            sbFromStart.InsertByIndex(insertIndexFromStart, insertValue, StringBuilderArray.StringBuilderArray.IndexDirection.FromStart);
            var indexFromEnd = expectStart.Length - insertIndexFromStart;
            sbFromEnd.InsertByIndex(indexFromEnd, insertValue, StringBuilderArray.StringBuilderArray.IndexDirection.FromEnd);

            var index = 0;
            foreach (var charValue in sbFromEnd)
            {
                Assert.That(charValue, Is.EqualTo(expectedResult[index++]));
            }

            index = 0;
            foreach (var charValue in sbFromStart)
            {
                Assert.That(charValue, Is.EqualTo(expectedResult[index++]));
            }
        }

#if NET6_0_OR_GREATER
        [Test]
        public void InsertInterpool()
        {
            var literal = "top";
        }
#endif
    }
}