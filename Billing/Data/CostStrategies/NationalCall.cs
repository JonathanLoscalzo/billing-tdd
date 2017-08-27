using Billing.Entities.Models;
using Billing.Data.Contracts;

namespace Billing.Data.CostStrategies
{
    public class NationalCall : DestinationCall
    {
        private readonly ICostRepository costRepository;
        
        public NationalCall(ICostRepository costRepository)
        {
            this.costRepository = costRepository;
        }
        public override double GetTax(Call call)
        {
            return this.costRepository.GetCostFromNationalCall(call.Receiver.Address);
        }
    }
}