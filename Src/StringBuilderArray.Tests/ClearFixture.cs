using NUnit.Framework;

namespace StringBuilderArrayTests
{
    [TestFixture]
    public class ClearFixture
    {
        [Test]
        [CancelAfter(5_000)]
        public void CallMultipleClear()
        {
            var sbArr = new StringBuilderArray.StringBuilderArray(5);
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    var str = j.ToString();
                    sbArr.Append(str);
                }

                sbArr.Clear();
                Assert.That(sbArr.Length, Is.EqualTo(0));
            }
        }
    }
}