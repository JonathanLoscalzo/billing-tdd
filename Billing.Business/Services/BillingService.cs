using Billing.Business.Services.Contracts;
using Billing.Business.Models;
using Billing.Business.Helpers;

namespace Billing.Business.Services
{
    public class BillingService : IBillingService
    {
        public Bill CreateBillFrom(Client client, int month) => new Bill()
        {
            Client = client.FullName,
            MontlyPrice = client.MontlyPrice,
            Month = (Months)month,
            NationalCallCost = client.GetNationalCost(),
            LocalCallCost = client.GetLocalCost(),
            InternationalCallCost = client.GetInternationalCost()
        };
    }
}