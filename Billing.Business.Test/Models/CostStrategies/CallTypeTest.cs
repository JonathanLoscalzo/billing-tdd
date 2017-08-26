using Xunit;
using System;
using FluentAssertions;
using Billing.Business.Helpers;
using Billing.Business.Models.CostStrategies;

namespace Billing.Business.Test.Models.CostStrategies
{
    public class ICallTypeTest
    {
        [Theory]
        [InlineData(Calls.International, typeof(InternationalCall))]
        [InlineData(Calls.Local, typeof(LocalCall))]
        [InlineData(Calls.National, typeof(NationalCall))]
        public void FactoryReturnStrategy(Calls calls, Type type)
        {
            CallType.Factory(calls).GetType().Should().Be(type);
        }
    }
}