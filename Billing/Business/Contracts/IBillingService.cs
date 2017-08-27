using Billing.Entities.Models;

namespace Billing.Business.Contracts
{
    public interface IBillingService
    {
        /// Crea una Facturación para un cliente, en determinado mes.
        Bill CreateBillFrom(Client client, int month);
    }
}