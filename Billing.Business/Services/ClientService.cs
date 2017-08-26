using System;
using System.Linq;
using Billing.Business.Models;
using Billing.Business.Services.Contracts;
using Billing.Business.Helpers;

namespace Billing.Business.Services
{
    public class ClientService : IClientService
    {
        public void AddCall(Client from, Client to, int duration, DateTime start)
        {
            var call = new Call()
            {
                //CallType = from.GetCallType(to),
                Transmitter = from,
                Receiver = to,
                StartTime = start,
                Duration = duration
            };

            from.Calls.ToList().Add(call);
        }
    }
}