using Xunit;
using Billing.Business.Models;
using FluentAssertions;

namespace Billing.Business.Test.Models
{
    [Trait("Category", "Models")]
    public class BillTest
    {
        [Theory]
        [InlineData(10.2, 20.2, 12.5, 29)]
        public void TotalCostReturnSumFromAllCosts(
            double localCost,
            double nationalCost,
            double internationalCost,
            double montlyCost)
        {
            var totalCost = localCost + nationalCost + internationalCost + montlyCost;
            var bill = new Bill()
            {
                MontlyPrice = montlyCost,
                NationalCallCost = nationalCost,
                InternationalCallCost = internationalCost,
                LocalCallCost = localCost
            };

            bill.TotalCost.Should().Be(totalCost);
        }
    }
}