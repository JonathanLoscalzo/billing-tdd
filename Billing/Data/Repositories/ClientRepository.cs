using System;
using System.Linq;
using Billing.Data.Contracts;
using Billing.Entities.Models;

namespace Billing.Data.Repositories
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        private readonly ICallRepository callRepository;
        private readonly IAddressRepository addressRepository;

        /// Remover cuando sea real
        protected override void Seed()
        {
            var f = new Bogus.Faker();

            var clients = ModelFakers.ClientFaker.Generate(10).ToList();
            clients.ForEach(c => c.Address = f.PickRandom(this.addressRepository.List()));

            var firstClients = clients
                .Take(5)
                .ToList();
            
            var lastClients = clients.Skip(5).Take(5).ToList();

            firstClients.ForEach(x => lastClients.ForEach(y => this.AddCall(x, y, f.Random.Int(0, 15), DateTime.Now)));
            lastClients.ForEach(x => firstClients.ForEach(y => this.AddCall(x, y, f.Random.Int(0, 15), DateTime.Now)));
            this.Create(clients);
        }

        public ClientRepository(ICallRepository callRepository, IAddressRepository addressRepository)
        {
            this.callRepository = callRepository;
            this.addressRepository = addressRepository;
            this.Seed();
        }

        public void AddCall(Client from, Client to, int duration, DateTime start)
        {
            var call = new Call()
            {
                Transmitter = from,
                Receiver = to,
                StartTime = start,
                Duration = duration
            };

            from.Calls.ToList().Add(call);

            this.CreateOrUpdate(from);
            this.callRepository.Create(call);
        }
    }
}