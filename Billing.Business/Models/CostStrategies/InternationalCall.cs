using System;

namespace Billing.Business.Models.CostStrategies
{
    public class InternationalCall : ICallType
    {
        ///  Las llamadas Internacionales tienen un costo distinto según el país al que se llame
        public override double HowMuchCost(Call call)
        {
            throw new NotImplementedException();
        }
    }
}