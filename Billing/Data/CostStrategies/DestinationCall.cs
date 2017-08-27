using Billing.Entities.Models;
using Billing.Entities.Enums;
using Billing.Data.Repositories;

namespace Billing.Data.CostStrategies
{
    public abstract class DestinationCall
    {
        public double HowMuchCost(Call call) => this.HowMuch(this.GetTax(call), call.Duration);

        protected double HowMuch(double tax, int duration) => tax * duration;

        public abstract double GetTax(Call call);

        public static DestinationCall GetInstance(Calls callType) {
            switch (callType)
            {
                case Calls.Local: return new LocalCall();
                case Calls.International: return new InternationalCall(new CostRepository());
                case Calls.National: return new NationalCall(new CostRepository());
            }

            return null;
        }
    }
}