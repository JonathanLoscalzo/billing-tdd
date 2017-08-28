using Billing.Data.Contracts;
using Billing.Entities.Models;

namespace Billing.Data.Repositories
{
    public class BillingRepository : Repository<Bill>, IBillRepository
    {
        protected override void Seed()
        {
        }
    }
}