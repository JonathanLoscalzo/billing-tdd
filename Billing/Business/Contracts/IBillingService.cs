using Billing.Entities.Enums;
using Billing.Entities.Models;

namespace Billing.Business.Contracts
{
    public interface IBillingService
    {
        /// Crea una Facturación para un cliente, en determinado mes.
        Bill CreateBillFrom(Client client, Months month, int year);

        Bill CreateBillFrom(int clientId, int month, int year);
    }
}