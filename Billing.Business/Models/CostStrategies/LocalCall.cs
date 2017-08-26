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
            return 0;
        }
    }
}