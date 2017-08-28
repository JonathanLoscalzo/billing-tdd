using System;
using System.Collections.Generic;

using Bogus;
using BAddress = Billing.Entities.Models.Address;
using Billing.Data.Contracts;
using Billing.Entities.Models;
using System.Linq;

namespace Billing.Data.Repositories
{
    public class CostRepository : ICostRepository
    {
        private IAddressRepository addressRepository;

        public Dictionary<string, double> InternationalCost { get; }
        public Dictionary<BAddress, double> NationalCost { get; }

        public CostRepository(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
            this.InternationalCost = new Dictionary<string, double>();
            this.NationalCost = new Dictionary<BAddress, double>();
            this.Seed();
        }

        public double GetCostFromNationalCall(BAddress address)
        {
            return this.NationalCost.ContainsKey(address)
            ? this.NationalCost[address]
            : 0.3;
        }

        public double GetCostFromInternationalCall(BAddress address)
        {
            return this.InternationalCost.ContainsKey(address.Country)
            ? this.InternationalCost[address.Country]
            : 0.3;
        }

        protected void Seed()
        {
            //Set the randomzier seed if you wish to generate repeatable data sets.
            Randomizer.Seed = new Random(8675309);

            var fakerAddress = this.addressRepository.List();
            var fakerCountries = fakerAddress.ToList().Select(a => a.Country);

            var faker = new Faker("es");

            foreach (var country in fakerCountries)
            {
                if (!this.InternationalCost.ContainsKey(country.ToString()))
                    this.InternationalCost.Add(country.ToString(), double.Parse(faker.Commerce.Price(0.1m, 1, 2)));
            }

            foreach (var address in fakerAddress)
            {
                if (!this.NationalCost.ContainsKey(address))
                    this.NationalCost.Add(address, double.Parse(faker.Commerce.Price(0.1m, 1, 2)));
            }
        }

        public AddressTax Create(AddressTax entity)
        {
            throw new NotImplementedException();
        }

        public void Create(IEnumerable<AddressTax> entities)
        {
            throw new NotImplementedException();
        }

        public AddressTax Read(int entityId)
        {
            throw new NotImplementedException();
        }

        public void Update(AddressTax entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int addressId)
        {
            var address = this.addressRepository.Read(addressId);
            if (address != null)
            {
                this.NationalCost.Remove(address);
            }
        }

        public IEnumerable<AddressTax> List()
        {
            throw new NotImplementedException();
        }
    }
}
