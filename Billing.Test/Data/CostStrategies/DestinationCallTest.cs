using Xunit;
using System;
using FluentAssertions;

using Billing.Entities.Enums;
using Billing.Data.CostStrategies;

namespace Billing.Test.Data.CostStrategies
{
    [Trait("Category", "CostStrategies")]
    public class DestinationCallTest
    {
        [Theory]
        [InlineData(Calls.International, typeof(InternationalCall))]
        [InlineData(Calls.Local, typeof(LocalCall))]
        [InlineData(Calls.National, typeof(NationalCall))]
        public void FactoryReturnStrategy(Calls calls, Type type)
        {
            DestinationCall.GetInstance(calls).GetType().Should().Be(type);
        }
    }
}