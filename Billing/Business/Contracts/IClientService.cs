using Billing.Entities.Enums;
using Billing.Entities.Models;

namespace Billing.Business.Contracts
{
    public interface IClientService
    {
        double GetNationalCost(Client client, Months month, int year);

        double GetInternationalCost(Client client, Months month, int year);

        double GetLocalCost(Client client, Months month, int year);

        //Retorna un Cliente
        Client GetClient(int id);
        
        /// Crea un cliente, a partir de una dirección existente
        Client Create(string name, string lastname, int addressId, double montlyPrice = 100, int phoneNumber = 0);
    }
}