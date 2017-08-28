using Billing.Business.Contracts;
using Billing.Business.Services;
using Billing.Data;
using Billing.Data.Contracts;
using Billing.Entities.Enums;
using Billing.Entities.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace Billing.Test.Business
{
    [Trait("Category", "Business")]
    public class BillingServiceTest
    {
        private Client client;
        private Mock<IClientService> clientService;
        private BillingService billService;

        public BillingServiceTest()
        {
            this.client = ModelFakers.ClientFaker.Generate(1)[0];

            this.clientService = new Mock<IClientService>();
            var billRepository = new Mock<IBillRepository>();
            billRepository.Setup(x => x.Create(It.IsAny<Bill>())).Returns((Bill a) => a);

            this.clientService
                .Setup(x => x.GetClient(this.client.Id))
                .Returns(this.client);
            this.clientService
                .Setup(x => x.GetNationalCost(It.IsAny<Client>(), It.IsAny<Months>(), It.IsAny<int>()))
                .Returns(10);
            this.clientService
                .Setup(x => x.GetInternationalCost(It.IsAny<Client>(), It.IsAny<Months>(), It.IsAny<int>()))
                .Returns(10);
            this.clientService
                .Setup(x => x.GetLocalCost(It.IsAny<Client>(), It.IsAny<Months>(), It.IsAny<int>()))
                .Returns(10);

            this.billService = new BillingService(this.clientService.Object, billRepository.Object);
        }

        [Fact]
        public void CreateBillFromClientId()
        {
            var bill = this.billService.CreateBillFrom(client.Id, (int)Months.January, 2017);

            bill.Should().NotBeNull();
            bill.LocalCallCost.Should().Be(10);
            bill.InternationalCallCost.Should().Be(10);
            bill.NationalCallCost.Should().Be(10);
            bill.Month.Should().Be(Months.January);
            bill.Year.Should().Be(2017);
            bill.MontlyPrice.Should().Be(client.MontlyPrice);
            bill.Client.Should().Contain(client.Name).And.Contain(client.Profile.ToString()).And.Contain(client.LastName);
        }

        [Fact]
        public void CreateBillFromClient()
        {
            var bill = this.billService.CreateBillFrom(client, Months.January, 2017);

            bill.Should().NotBeNull();
            bill.LocalCallCost.Should().Be(10);
            bill.InternationalCallCost.Should().Be(10);
            bill.NationalCallCost.Should().Be(10);
            bill.Month.Should().Be(Months.January);
            bill.Year.Should().Be(2017);
            bill.MontlyPrice.Should().Be(client.MontlyPrice);
            bill.Client.Should().Contain(client.Name).And.Contain(client.Profile.ToString()).And.Contain(client.LastName);
        }
    }
}