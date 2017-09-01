using System;
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
        private Mock<IAddressRepository> addressRepo;
        private ClientService clientService;
        private Client client;

        public ClientServiceTest()
        {
            this.callService = new Mock<ICallService>();

            this.clientRepository = new Mock<IClientRepository>();
            this.addressRepo = new Mock<IAddressRepository>();
            this.clientService = new ClientService(this.callService.Object, this.clientRepository.Object, this.addressRepo.Object);
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

        [Fact]
        public void WhenCreateReturnsCreated()
        {
            //Given
            var address = ModelFakers.AddressFaker.Generate(1)[0];
            this.clientRepository
                .Setup(x => x.Create(It.IsAny<Client>()))
                .Returns((Client c) => c);

            //When
            this.addressRepo.Setup(x => x.Read(1)).Returns(address);

            var result = this.clientService.Create("name", "lastname", 1, 232, 123241);

            //Then
            result.Address.Should().Be(address);
            result.Calls.Should().BeEmpty();
            result.Name.Should().Be("name");
            result.LastName.Should().Be("lastname");
            result.PhoneNumber.Should().Be(123241);
            result.MontlyPrice.Should().Be(232);
        }

        [Fact]
        public void WhenAddCallToExistentClientsSholdInvokeAddCallFromRepository()
        {
            //Given
            var clients = ModelFakers.ClientFaker.Generate(2);
            const int duration = 12;
            this.clientRepository.Setup(c => c.Read(It.IsAny<int>())).Returns((int id) => clients.FirstOrDefault(x => x.Id == id));
            this.clientRepository.Setup(x => x.AddCall(It.IsAny<Client>(), It.IsAny<Client>(), duration, It.IsAny<DateTime>())).Verifiable("Not execute");

            //When
            this.clientService.AddCallTo(clients[0].Id, clients[1].Id, duration);

            //Then
            this.clientRepository.VerifyAll();
        }
    }
}