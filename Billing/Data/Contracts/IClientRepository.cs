using System;
using Billing.Entities.Models;

namespace Billing.Data.Contracts
{
    public interface IClientRepository : IRepository<Client>
    {
        void AddCall(Client from, Client to, int duration, DateTime now);
    }
}