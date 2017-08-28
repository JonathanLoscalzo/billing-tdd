using Billing.Data.Contracts;
using Billing.Entities.Models;

namespace Billing.Data.Repositories
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        protected override void Seed()
        {
            this.Create(ModelFakers.AddressFaker.Generate(100));
        }

        public AddressRepository()
        {
            this.Seed();
        }
    }
}