using Billing.Data;
using Billing.Data.Contracts;
using Billing.Data.Repositories;
using Billing.Entities.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace Billing.Test.Data.Repositories
{
    /// Se podría hacer una clase de test para Repositorio solamente
    public class ClientRepositoryTest
    {
        private ClientRepository clientRepository;

        public ClientRepositoryTest()
        {
            var callRepository = new Mock<ICallRepository>();
            this.clientRepository = new ClientRepository(callRepository.Object);
        }

        [Fact]
        public void WhenCreateHasSeedData() => this.clientRepository.List().Should().NotBeEmpty();

        [Fact]
        public void WhenCreateCouldRetrieve()
        {
            var client = ModelFakers.ClientFaker.Generate(1)[0];

            this.clientRepository.Create(client);
            var result = this.clientRepository.Read(client.Id);

            result.Should().NotBeNull();
            result.Should().BeOfType<Client>();
        }

        [Fact]
        public void WhenModifyClientRetrieveOk()
        {
            var client = ModelFakers.ClientFaker.Generate(1)[0];

            this.clientRepository.Create(client);
            var result = this.clientRepository.Read(client.Id);

            client.Name = "NEW NAME";

            this.clientRepository.Update(client);
            var result2 = this.clientRepository.Read(client.Id);

            result.Name.Should().NotBe(result2.Name, "Deberian ser distintos nombres!");
            result2.Name.Should().Be(client.Name, "Debería contener el nuevo nombre! {0}", client.Name);
        }

        [Fact]
        public void WhenRemoveClientNotRetrieve()
        {
            var client = ModelFakers.ClientFaker.Generate(1)[0];

            this.clientRepository.Create(client);
            var result = this.clientRepository.Read(client.Id);
            this.clientRepository.Delete(client.Id);
            var result2 = this.clientRepository.Read(client.Id);

            result.Id.Should().Be(client.Id);
            result2.Should().BeNull();
        }
    }
}