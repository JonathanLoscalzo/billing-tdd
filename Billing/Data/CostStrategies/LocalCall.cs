using System;
using Billing.Entities.Models;

namespace Billing.Data.CostStrategies
{
    public class LocalCall : DestinationCall
    {
        public override double GetTax(Call call)
        {
            switch (call.StartTime.DayOfWeek)
            {
                case DayOfWeek.Friday:
                case DayOfWeek.Monday:
                case DayOfWeek.Thursday:
                case DayOfWeek.Wednesday:
                case DayOfWeek.Tuesday:
                    var hour = call.StartTime.Hour;
                    if (hour >= 8 && hour <= 20)
                    {
                        return 0.20;
                    }
                    else
                    {
                        return 0.10;
                    }
                case DayOfWeek.Saturday:
                case DayOfWeek.Sunday:
                    return 0.10;
            }

            return 0;
        }
    }
}