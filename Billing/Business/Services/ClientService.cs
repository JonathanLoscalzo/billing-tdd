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

        public ClientService(
            ICallService callService,
            IClientRepository clientRepository)
        {
            this.callService = callService;
            this.clientRepository = clientRepository;
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
    }
}