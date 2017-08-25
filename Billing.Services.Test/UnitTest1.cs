using System;
using Xunit;
using Billing.Services;

namespace Billing.Services.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.Throws<Exception>(() => new Class1());
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(7)]
        public void ReturnTrueGivenPrimesLessThan10(int value)
        {
            //  var result = _primeService.IsPrime(value); 
            var result = false;
            Assert.True(result, $"{value} should be prime");
        }
    }
}
