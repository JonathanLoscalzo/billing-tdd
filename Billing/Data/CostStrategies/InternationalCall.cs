using Billing.Entities.Models;
using Billing.Data.Contracts;

namespace Billing.Data.CostStrategies
{
    public class InternationalCall : DestinationCall
    {
        private readonly ICostRepository costRepository;

        public InternationalCall(ICostRepository costRepository)
        {
            //TODO: IOC/ID
            this.costRepository = costRepository;
        }

        public override double GetTax(Call call)
        {
            return this.costRepository.GetCostFromInternationalCall(call.Receiver.Address);
        }
    }
}