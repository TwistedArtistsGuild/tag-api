using Xunit;

namespace tagApiUnitTests
{
    public class AlwaysTrue
    {
        public AlwaysTrue() // Constructor name fixed
        {
        }

        [Fact]
        public void TestAlwaysTrue()
        {
            Assert.True(true);
        }
    }
}