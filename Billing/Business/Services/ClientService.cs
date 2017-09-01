using System;
using System.Linq;

using Billing.Business.Contracts;
using Billing.Data.Contracts;
using Billing.Entities.Enums;
using Billing.Entities.Models;

namespace Billing.Business.Services
{
    public class ClientService : IClientService
    {
        private readonly ICallService callService;
        private readonly IClientRepository clientRepository;
        private readonly IAddressRepository addressRepo;

        public ClientService(
            ICallService callService,
            IClientRepository clientRepository,
            IAddressRepository addressRepository)
        {
            this.callService = callService;
            this.clientRepository = clientRepository;
            this.addressRepo = addressRepository;
        }

        public double GetNationalCost(Client client, Months month, int year) => this.GetCostFrom(client, Calls.National, month, year);

        public double GetInternationalCost(Client client, Months month, int year) => this.GetCostFrom(client, Calls.International, month, year);

        public double GetLocalCost(Client client, Months month, int year) => this.GetCostFrom(client, Calls.Local, month, year);

        private double GetCostFrom(Client client, Calls callType, Months month, int year)
        {
            // TODO: Refactor, envidia de atributo
            return client
                .Calls
                .Where(c => this.callService.CallType(c) == callType && c.StartTime.Month == (int)month && c.StartTime.Year == year)
                .Sum(v => this.callService.Cost(v));
        }

        public Client GetClient(int id) => this.clientRepository.Read(id);

        public Client Create(string name, string lastname, int addressId, double montlyPrice = 100, int phoneNumber = 0)
        {
            var client = new Client()
            {
                Name = name,
                LastName = lastname,
                MontlyPrice = montlyPrice,
                PhoneNumber = phoneNumber,
                Address = this.addressRepo.Read(addressId)
            };

            return this.clientRepository.Create(client);
        }

        public void AddCallTo(int fromId, int toId, int duration)
        {
            var from = this.clientRepository.Read(fromId);
            var to = this.clientRepository.Read(toId);
            if (from != null && to != null)
            {
                this.clientRepository.AddCall(from, to, duration, DateTime.Now);
            }
        }
    }
}