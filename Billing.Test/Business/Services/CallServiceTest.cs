using System;
using Billing.Business.Contracts;
using Billing.Business.Services;
using Billing.Data;
using Billing.Data.Contracts;
using Billing.Data.CostStrategies;
using Billing.Entities.Enums;
using Billing.Entities.Models;
using Bogus;
using FluentAssertions;
using Moq;
using Xunit;

namespace Billing.Test.Business.Services
{
    public class CallServiceTest
    {
        private ICallService callService;

        public CallServiceTest()
        {
            var costRepository = new Mock<ICostRepository>();
            costRepository
                .Setup(x => x.GetCostFromInternationalCall(It.IsAny<Address>()))
                .Returns(3);

            this.callService = new CallService(costRepository.Object);
        }

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
            };

            this.callService.CallType(call).Should().BeOfType<Calls>();
            this.callService.CallType(call).Should().Be(Calls.Local);
            this.callService.DestionationCall(call).Should().BeOfType<LocalCall>();
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

            this.callService.CallType(call).Should().BeOfType<Calls>();
            this.callService.CallType(call).Should().Be(Calls.International);
            this.callService.DestionationCall(call).Should().BeOfType<InternationalCall>();
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

            this.callService.CallType(call).Should().BeOfType<Calls>();
            this.callService.CallType(call).Should().Be(Calls.National);
            this.callService.DestionationCall(call).Should().BeOfType<NationalCall>();
        }

        [Theory]
        [InlineData(Calls.International, typeof(InternationalCall))]
        [InlineData(Calls.Local, typeof(LocalCall))]
        [InlineData(Calls.National, typeof(NationalCall))]
        public void FactoryReturnStrategy(Calls calls, Type type)
        {
            this.callService.GetInstance(calls).GetType().Should().Be(type);
        }
    }
}