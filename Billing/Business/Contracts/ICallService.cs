using Billing.Data.CostStrategies;
using Billing.Entities.Enums;
using Billing.Entities.Models;

namespace Billing.Business.Contracts
{
    public interface ICallService
    {
        /// Retorna el tipo de Llamada.
        Calls CallType(Call call);

        /// Retorna la estrategia de resolución del costo
        DestinationCall DestionationCall(Call call);

        /// Devuelve el costo de una llamada, internamente debe saber que tipo de llamada es
        double Cost(Call call);

        /// TODO: lo dejo publico, para poder testearlo, aunque Moq tiene una manera de testear methodos Protected. 
        /// Devuelve la estrategia de resolución del costo, dependiento del tipo de llamada
        DestinationCall GetInstance(Calls callType);
    }
}