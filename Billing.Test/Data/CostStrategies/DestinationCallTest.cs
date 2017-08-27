using Xunit;
using FluentAssertions;

using Billing.Data.CostStrategies;
using Moq;
using Billing.Entities.Models;
using Billing.Data;

namespace Billing.Test.Data.CostStrategies
{
    [Trait("Category", "CostStrategies")]
    public class DestinationCallTest
    {
        private Mock<DestinationCall> strategy;

        public DestinationCallTest()
        {
            this.strategy = new Mock<DestinationCall>();
            this.strategy
                .Setup(s => s.GetTax(It.IsAny<Call>()))
                .Returns(2)
                .Verifiable("No se invocó");
        }

        [Fact]
        public void VerifyHowMuchCostCorrectAlgorithm()
        {
            var call = ModelFakers.CallFaker.Generate(1)[0];
            call.Duration = 12;

            var result = this.strategy.Object.HowMuchCost(call);

            result.Should().Be(12 * 2);
        }
    }
}