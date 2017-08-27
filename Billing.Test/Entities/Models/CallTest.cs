using Xunit;
using FluentAssertions;
using Bogus;

using Billing.Entities.Models;
using Billing.Entities.Enums;

using Billing.Data;
using Billing.Data.CostStrategies;


namespace Billing.Test.Entities.Models
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
            }; ;

            call.CallType.Should().BeOfType<Calls>();
            call.CallType.Should().Be(Calls.Local);
            call.DestionationCall.Should().BeOfType<LocalCall>();
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
            call.DestionationCall.Should().BeOfType<InternationalCall>();
        }

        [Theory]
        [InlineData("locality-diff", "province-diff")]
        [InlineData("locality-diff", "province")]
        [InlineData("locality", "province-diff")]
        public void SameCountryDiffLocalityReturnNationalCallType(string locality, string province)
        {
            var country = new Faker("es").Address.Country();
            var localityAux = "locality";
            var provinceAux = "provinceAux";

            var call = ModelFakers.CallFaker.Generate(1)[0];

            call.Receiver.Address = new Address()
            {
                Country = country,
                Locality = localityAux,
                Province = provinceAux
            };

            call.Transmitter.Address = new Address()
            {
                Country = country,
                Locality = locality,
                Province = province
            };

            call.CallType.Should().BeOfType<Calls>();
            call.CallType.Should().Be(Calls.National);
            call.DestionationCall.Should().BeOfType<NationalCall>();
        }
    }
}