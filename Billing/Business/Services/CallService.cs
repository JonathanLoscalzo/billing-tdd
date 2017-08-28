using Billing.Business.Contracts;
using Billing.Data.Contracts;
using Billing.Data.CostStrategies;
using Billing.Data.Helpers;
using Billing.Entities.Enums;
using Billing.Entities.Models;

namespace Billing.Business.Services
{
    public class CallService : ICallService
    {
        private readonly ICostRepository costRepository;

        public CallService(ICostRepository costRepository)
        {
            this.costRepository = costRepository;
        }

        public Calls CallType(Call call) => call.Transmitter.GetCallType(call.Receiver);

        public DestinationCall DestionationCall(Call call) => this.GetInstance(this.CallType(call));

        public double Cost(Call call) => this.DestionationCall(call).HowMuchCost(call);

        public DestinationCall GetInstance(Calls callType)
        {
            switch (callType)
            {
                case Calls.Local: return new LocalCall();
                case Calls.International: return new InternationalCall(this.costRepository);
                case Calls.National: return new NationalCall(this.costRepository);
            }

            return null;
        }
    }
}