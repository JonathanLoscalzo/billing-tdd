using Billing.Entities.Models;
using FluentAssertions;
using Xunit;

namespace Billing.Test.Entities.Models
{
    public class AddressTaxTest
    {
        [Fact(Skip = "Upcasting oculta métodos")]
        public void WhenUpcastingNotReturnsAddressId()
        {
            var address = new Address();

            var tax = new AddressTax()
            {
                Address = address,
                Tax = 100
            };

            ((Guid)tax).Id.Should().NotBe(address.Id);
        }

        [Fact]
        public void EqualsAddressTaxByAddress()
        {
            var address = new Address();

            var tax = new AddressTax()
            {
                Address = address,
                Tax = 100
            };

            var tax2 = new AddressTax()
            {
                Address = address,
                Tax = 10000
            };

            tax.Equals(tax2).Should().BeTrue();
        }
    }
}