using Billing.Entities.Models;

namespace Billing.Data.CostStrategies
{
    public abstract class DestinationCall
    {
        public double HowMuchCost(Call call) => this.HowMuch(this.GetTax(call), call.Duration);

        protected double HowMuch(double tax, int duration) => tax * duration;

        public abstract double GetTax(Call call);
    }
}