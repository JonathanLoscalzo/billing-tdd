using Billing.Business.Contracts;
using Billing.Data.Contracts;
using Billing.Data.Repositories;
using Billing.Entities.Models;

namespace Billing.Business.Services
{
    /// Implementación Mock
    public class CostExternalService : ICostExternalService
    {
        public readonly ICostRepository costRepository;

        public CostExternalService(ICostRepository costRepository)
        {
            this.costRepository = costRepository;
        }

        public double GetCostFromNationalCall(Address address)
        {
            return this.costRepository.GetCostFromNationalCall(address);
        }

        public double GetCostFromInternationalCall(Address address)
        {
            return this.costRepository.GetCostFromInternationalCall(address);
        }
    }
}