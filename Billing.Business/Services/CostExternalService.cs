using Billing.Business.Models;
using Billing.Business.Services.Contracts;

namespace Billing.Business.Services
{
    public class CostExternalService : ICostExternalService
    {
        public double GetCostFromNationalCall(Address address)
        {
            return 0;
        }

        public double GetCostFromInternationalCall(Address address)
        {
            return 0;
        }
    }
}