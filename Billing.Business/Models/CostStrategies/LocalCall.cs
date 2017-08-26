using System;

namespace Billing.Business.Models.CostStrategies
{
    public class LocalCall : ICallType
    {
        /**
        Las llamadas locales tienen distintos valores según la franja horaria 
        en la que se realizan y el día. 
        Para los días hábiles, de 8 a 20 hrs. el costo es de 0,20 centavos el minuto, 
        mientras en el resto de las horas es de 0,10 centavos el minuto. 
        Los sábados y domingos cuesta 0,10 centavos el minuto
         */
        public override double HowMuchCost(Call call)
        {
            var hour = call.StartTime.Hour;
            var tax = 0.10m;

            var day = call.StartTime.DayOfWeek;
            switch (day)
            {
                case DayOfWeek.Friday:
                case DayOfWeek.Monday:
                case DayOfWeek.Thursday:
                case DayOfWeek.Wednesday:
                case DayOfWeek.Tuesday:
                    if (hour >= 8 && hour <= 20)
                    {
                        tax = 0.20m;
                    }
                    break;
                case DayOfWeek.Saturday:
                case DayOfWeek.Sunday:
                    tax = 0.10m;
                    break;
            }
            
            return HowMuch((double)tax, call.Duration);
        }
    }
}