using System.Linq;
using Billing.Business.Contracts;
using Billing.Business.Services;
using Billing.Data;
using Billing.Data.Contracts;
using Billing.Entities.Enums;
using Billing.Entities.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace Billing.Test.Business.Services
{
    public class ClientServiceTest
    {
        private Mock<ICallService> callService;
        private Mock<IClientRepository> clientRepository;
        private ClientService clientService;
        private Client client;

        public ClientServiceTest()
        {
            this.callService = new Mock<ICallService>();

            this.clientRepository = new Mock<IClientRepository>();
            this.clientService = new ClientService(this.callService.Object, this.clientRepository.Object);
            var f = new Bogus.Faker("es");
            this.client = new Client()
            {
                Profile = f.Random.Int(10000, 99999),
                Name = f.Person.FirstName,
                LastName = f.Person.LastName,
                MontlyPrice = double.Parse(f.Commerce.Price(decimals: 2)),
                Address = ModelFakers.AddressFaker.Generate(1)[0],
            };

            var calls = ModelFakers.CallFaker.Generate(10).ToList();
            calls.ForEach(c =>
            {
                c.StartTime = 1.January(2017);
                this.client.Calls.Add(c);
            });

            var calls2 = ModelFakers.CallFaker.Generate(5).ToList();
            calls2.ForEach(c =>
            {
                c.StartTime = 1.February(2017);
                this.client.Calls.Add(c);
            });
        }

        [Theory]
        [InlineData(Months.January, 2017, 10 * 10)]
        [InlineData(Months.February, 2017, 5 * 10)]
        [InlineData(Months.March, 2017, 0)]
        public void WhenGetLocalCostReturnsOnlyLocalCost(Months month, int year, double totalCost)
        {
            this.callService
                .Setup(x => x.CallType(It.IsAny<Call>()))
                .Returns(Calls.Local);
            this.callService
                .Setup(x => x.Cost(It.IsAny<Call>()))
                .Returns(10);

            var result = this.clientService.GetLocalCost(client, month, year);

            result.Should().Be(totalCost);
        }

        [Theory]
        [InlineData(Months.January, 2017, 10 * 10)]
        [InlineData(Months.February, 2017, 5 * 10)]
        [InlineData(Months.March, 2017, 0)]
        public void WhenGetInternationalCostReturnsOnlyInternationalCost(Months month, int year, double totalCost)
        {
            this.callService
                .Setup(x => x.CallType(It.IsAny<Call>()))
                .Returns(Calls.International);
            this.callService
                .Setup(x => x.Cost(It.IsAny<Call>()))
                .Returns(10);

            var result = this.clientService.GetInternationalCost(client, month, year);

            result.Should().Be(totalCost);
        }

        [Theory]
        [InlineData(Months.January, 2017, 10 * 10)]
        [InlineData(Months.February, 2017, 5 * 10)]
        [InlineData(Months.March, 2017, 0)]
        public void WhenGetNationalCostReturnsOnlyGetNationalCost(Months month, int year, double totalCost)
        {
            this.callService
                .Setup(x => x.CallType(It.IsAny<Call>()))
                .Returns(Calls.National);
            this.callService
                .Setup(x => x.Cost(It.IsAny<Call>()))
                .Returns(10);

            var result = this.clientService.GetNationalCost(client, month, year);

            result.Should().Be(totalCost);
        }
    }
}