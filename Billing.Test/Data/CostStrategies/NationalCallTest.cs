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
    public class NationalCallTest
    {
        private NationalCall nationalCallStrategy;
        private Mock<ICostRepository> costRepository;

        public NationalCallTest()
        {
            this.costRepository = new Mock<ICostRepository>();
            this.costRepository
                .Setup(x => x.GetCostFromNationalCall(It.IsAny<Address>()))
                .Returns(3)
                .Verifiable("No se Invocó");

            this.nationalCallStrategy = new NationalCall(costRepository.Object);
        }

        [Fact]
        public void ShouldInvokeGetCostFromNationalCall()
        {
            this.nationalCallStrategy.GetTax(ModelFakers.CallFaker.Generate(1)[0]).Should().Be(3);
            costRepository.VerifyAll();
        }
    }
}