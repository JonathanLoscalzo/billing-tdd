using System;
using System.Collections.Generic;

using Bogus;
using BAddress = Billing.Entities.Models.Address;
using Billing.Entities.Models;
using Billing.Data.Contracts;

namespace Billing.Data.Repositories
{
    public class CostRepository : ICostRepository
    {
        public Dictionary<string, double> InternationalCost { get; }
        public Dictionary<BAddress, double> NationalCost { get; }

        public CostRepository()
        {
            //Set the randomzier seed if you wish to generate repeatable data sets.
            Randomizer.Seed = new Random(8675309);

            this.InternationalCost = new Dictionary<string, double>();
            this.NationalCost = new Dictionary<BAddress, double>();

            var fakerCountries = new Faker<dynamic>("es")
            .Rules((f, s) =>
            {
                s = f.Address.Country();
            }).Generate(1000);

            var faker = new Faker("es");

            foreach (var country in fakerCountries)
            {
                if (!this.InternationalCost.ContainsKey(country.ToString()))
                    this.InternationalCost.Add(country.ToString(), double.Parse(faker.Commerce.Price(0.1m, 1, 2)));
            }

            var fakerAddress = ModelFakers.AddressFaker.Generate(1000);

            foreach (var address in fakerAddress)
            {
                if (!this.NationalCost.ContainsKey(address))
                    this.NationalCost.Add(address, double.Parse(faker.Commerce.Price(0.1m, 1, 2)));
            }
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

        public double Cost(Call call) => call.DestionationCall.HowMuchCost(call);
    }
}
