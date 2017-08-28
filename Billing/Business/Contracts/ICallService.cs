using Billing.Data.CostStrategies;
using Billing.Entities.Enums;
using Billing.Entities.Models;

namespace Billing.Business.Contracts
{
    public interface ICallService
    {
        Calls CallType(Call call);

        DestinationCall DestionationCall(Call call);

        double Cost(Call call);

        DestinationCall GetInstance(Calls callType);
    }
}