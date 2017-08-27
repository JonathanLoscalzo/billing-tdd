using Billing.Entities.Models;

namespace Billing.Data.Contracts
{
    public interface ICostRepository
    {
        double GetCostFromNationalCall(Address address);
        double GetCostFromInternationalCall(Address address);
    }
}