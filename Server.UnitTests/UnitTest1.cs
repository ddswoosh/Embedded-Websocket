using Xunit;
using FluentAssertions;
using Server.Controllers;

namespace Server.UnitTests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(1,2,3)]
        public void Testing(int a, int b, int c)
        {
            int res = a + b;
            res.Should().Be(c);
        }

        [Fact]
        public void helper()
        {
            int a = 1;
            a.Should().Be(1);   
        }    
    }


}