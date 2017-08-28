using Billing.Data.Contracts;
using Billing.Entities.Models;

namespace Billing.Data.Repositories
{
    public class CallRepository : Repository<Call>, ICallRepository
    {
        protected override void Seed()
        {
        }
    }
}