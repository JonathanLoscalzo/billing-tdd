using Billing.Business.Services;
using Billing.Business.Services.Contracts;

namespace Billing.Business.Models.CostStrategies
{
    public class InternationalCall : CallType
    {
        private readonly ICostExternalService costExternalService;

        public InternationalCall()
        {
            //TODO: IOC/ID
            this.costExternalService = new CostExternalService();
            
        }

        public override double GetTax(Call call)
        {
            return this.costExternalService.GetCostFromInternationalCall(call.Receiver.Address);
        }
    }
}