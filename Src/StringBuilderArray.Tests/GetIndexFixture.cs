using NUnit.Framework;

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

#if NET6_0_OR_GREATER
        [Test]
        public void GetIndexInterpool()
        {
            var sbArr = new StringBuilderArray.StringBuilderArray();
            for (int i = 0; i < 10; i++)
            {
                sbArr.Append($"Number {i}");
                Assert.That(sbArr.GetIndex($"Number {i}"), Is.EqualTo(0));
            }

            var index = 0;
            for (int i = 10 - 1; i >= 0; i--)
            {
                Assert.That(sbArr.GetIndex($"Number {i}"), Is.EqualTo((index++) * 2));
            }

            Assert.That(sbArr.GetIndex("20"), Is.EqualTo(-1));
        }
#endif
    }
}