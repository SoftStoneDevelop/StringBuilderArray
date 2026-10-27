using NUnit.Framework;

namespace StringBuilderArrayTests
{
    [TestFixture]
    public class LengthFixture
    {
        [Test]
        public void Length()
        {
            var sbArr = new StringBuilderArray.StringBuilderArray();
            var totalLength = 0;
            for (int i = 0; i < 10; i++)
            {
                var str = i.ToString();
                sbArr.Append(str);
                totalLength += str.Length;
                Assert.That(sbArr.Length, Is.EqualTo(totalLength));
            }

            Assert.That(sbArr.Length, Is.EqualTo(totalLength));
        }
    }
}