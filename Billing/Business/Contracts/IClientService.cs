using Billing.Entities.Enums;
using Billing.Entities.Models;

namespace Billing.Business.Contracts
{
    public interface IClientService
    {
        double GetNationalCost(Client client, Months month, int year);

        double GetInternationalCost(Client client, Months month, int year);

        double GetLocalCost(Client client, Months month, int year);

        Client GetClient(int id);
    }
}