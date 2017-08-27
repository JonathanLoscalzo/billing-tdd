using System;
using Billing.Entities.Models;

namespace Billing.Business.Contracts
{
    public interface IClientService
    {
        void AddCall(Client from, Client to, int duration, DateTime start);
    }
}