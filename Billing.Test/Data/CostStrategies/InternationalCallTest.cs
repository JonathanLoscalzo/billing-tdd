using Billing.Data;
using Billing.Data.Contracts;
using Billing.Data.CostStrategies;
using Billing.Entities.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace Billing.Test.Data.CostStrategies
{
    [Trait("Category", "CostStrategies")]
    public class InternationalCallTest
    {
        private InternationalCall strategy;
        private Mock<ICostRepository> costRepository;

        public InternationalCallTest()
        {
            this.costRepository = new Mock<ICostRepository>();
            this.costRepository
                .Setup(x => x.GetCostFromInternationalCall(It.IsAny<Address>()))
                .Returns(3)
                .Verifiable("No se Invocó");

            this.strategy = new InternationalCall(costRepository.Object);
        }

        [Fact(DisplayName = "ShouldInvokeGetCostFromNInternationalCall")]
        public void ShouldInvokeGetCostFromNInternationalCall()
        {
            this.strategy.GetTax(ModelFakers.CallFaker.Generate(1)[0]).Should().Be(3);
            costRepository.VerifyAll();
        }
    }
}