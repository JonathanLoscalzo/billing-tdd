using Billing.Business.Contracts;
using Billing.Entities.Models;
using Billing.Entities.Enums;

namespace Billing.Business.Services
{
    public class BillingService : IBillingService
    {
        public Bill CreateBillFrom(Client client, int month) => new Bill()
        {
            Client = $"{client.Profile} - {client.FullName}",
            MontlyPrice = client.MontlyPrice,
            Month = (Months)month,
            NationalCallCost = client.GetNationalCost(),
            LocalCallCost = client.GetLocalCost(),
            InternationalCallCost = client.GetInternationalCost()
        };
    }
}