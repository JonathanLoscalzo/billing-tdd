using Billing.Entities.Models;

namespace Billing.Data.Contracts
{
    public interface ICostRepository : IRepository<AddressTax>
    {
        double GetCostFromNationalCall(Address address);
        double GetCostFromInternationalCall(Address address);
    }
}