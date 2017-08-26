using System;
using Billing.Business.Services;
using Billing.Business.Services.Contracts;

namespace Billing.Business.Models.CostStrategies
{
    public class NationalCall : ICallType
    {
        private readonly ICostExternalService costExternalService;
        public NationalCall()
        {
            this.costExternalService = new CostExternalService();
        }
        public override double GetTax(Call call)
        {
            return this.costExternalService.GetCostFromNationalCall(call.Receiver.Address);
        }
    }
}