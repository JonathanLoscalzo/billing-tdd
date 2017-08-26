using System;
using Billing.Business.Models;

namespace Billing.Business.Services.Contracts
{
    public interface IClientService
    {
        void AddCall(Client from, Client to, int duration, DateTime start);
    }
}