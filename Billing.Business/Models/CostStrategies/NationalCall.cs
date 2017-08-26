using System;

namespace Billing.Business.Models.CostStrategies
{
    public class NationalCall : ICallType
    {
        ///  Las llamadas Nacionales tienen un costo distinto según la localidad a la que se llame
        public override double HowMuchCost(Call call)
        {
            throw new NotImplementedException();
        }
    }
}