using System;
using CallEnum = Billing.Business.Helpers.Calls;

namespace Billing.Business.Models.CostStrategies
{
    public abstract class ICallType
    {
        public double HowMuchCost(Call call) => this.HowMuch(this.GetTax(call), call.Duration);

        protected double HowMuch(double tax, int duration) => tax * duration;

        public abstract double GetTax(Call call);

        public static ICallType Factory(CallEnum callType)
        {
            switch (callType)
            {
                case CallEnum.Local: return new LocalCall();
                case CallEnum.International: return new LocalCall();
                case CallEnum.National: return new LocalCall();
            }

            return null;
        }
    }
}