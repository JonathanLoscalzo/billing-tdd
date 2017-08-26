using Billing.Business.Models;

namespace Billing.Business.Services.Contracts
{
    public interface IBillingService
    {
        /// Crea una Facturación para un cliente, en determinado mes.
        Bill CreateBillFrom(Client client, int month);
    }
}