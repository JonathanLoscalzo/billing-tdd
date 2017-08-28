using Billing.Business.Contracts;
using Billing.Entities.Models;
using Billing.Entities.Enums;
using Billing.Data.Contracts;

namespace Billing.Business.Services
{
    public class BillingService : IBillingService
    {
        private readonly IClientService clientService;
        private readonly IBillRepository billRepository;

        public BillingService(IClientService clientService, IBillRepository billRepository)
        {
            this.clientService = clientService;
            this.billRepository = billRepository;
        }

        public Bill CreateBillFrom(Client client, Months month, int year)
        {
            return this
                .billRepository
                .Create(
                    new Bill()
                    {
                        NationalCallCost = this.clientService.GetNationalCost(client, (Months)month, year),
                        LocalCallCost = this.clientService.GetLocalCost(client, (Months)month, year),
                        InternationalCallCost = this.clientService.GetInternationalCost(client, (Months)month, year),
                        Month = month,
                        Year = year,
                        Client = string.Format("{0} - {1}, {2}", client.Profile.ToString(), client.LastName, client.Name),
                        MontlyPrice = client.MontlyPrice,
                    });
        }

        public Bill CreateBillFrom(int clientId, int month, int year)
        {
            return this.CreateBillFrom(this.clientService.GetClient(clientId), (Months)month, year);
        }
    }
}