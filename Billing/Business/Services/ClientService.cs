using System;
using System.Linq;

using Billing.Business.Contracts;
using Billing.Entities.Models;

namespace Billing.Business.Services
{
    public class ClientService : IClientService
    {
        public void AddCall(Client from, Client to, int duration, DateTime start)
        {
            var call = new Call()
            {
                Transmitter = from,
                Receiver = to,
                StartTime = start,
                Duration = duration
            };

            from.Calls.ToList().Add(call);
        }
    }
}