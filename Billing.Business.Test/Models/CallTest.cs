using Xunit;
using System.Linq;
using FluentAssertions;
using Bogus;
using Billing.Business.Helpers;
using Billing.Business.Models;
using Billing.Business.Models.CostStrategies;

namespace Billing.Business.Test.Models
{
    [Trait("Category", "Models")]
    public class CallTest
    {
        [Fact]
        public void SameAddressReturnLocalCallType()
        {
            var address = ModelFakers.AddressFaker.Generate(1)[0];
            var call = ModelFakers.CallFaker.Generate(1)[0];

            call.Transmitter.Address = address;
            call.Receiver.Address = new Address()
            {
                Country = address.Country,
                Locality = address.Locality,
                Province = address.Province
            };;

            call.CallType.Should().BeOfType<Calls>();
            call.CallType.Should().Be(Calls.Local);
            call.CallStrategy.Should().BeOfType<LocalCall>();
        }

        [Fact]
        public void DiffCountryReturnInternationalCallType()
        {
            var address = ModelFakers.AddressFaker.Generate(1)[0];
            var call = ModelFakers.CallFaker.Generate(1)[0];

            call.Receiver.Address = address;
            
            call.Transmitter.Address = new Address()
            {
                Country = address.Country + " DIFF",
                Locality = address.Locality,
                Province = address.Province
            };

            call.CallType.Should().BeOfType<Calls>();
            call.CallType.Should().Be(Calls.International);
            call.CallStrategy.Should().BeOfType<InternationalCall>();
        }
    }
}