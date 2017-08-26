using Bogus;
using Billing.Business.Models;
using BAddress = Billing.Business.Models.Address;

namespace Billing.Business.Test.Models
{
    public static class ModelFakers
    {
        public static Faker<Client> ClientFaker = new Faker<Client>("es")
        .Rules((f, c) =>
        {
            c.Profile = f.UniqueIndex;
            c.Name = f.Person.FirstName;
            c.LastName = f.Person.LastName;
            c.MontlyPrice = double.Parse(f.Commerce.Price(decimals: 2));
            c.Address = ModelFakers.AddressFaker.Generate(1)[0];
            //c.Calls = ModelFakers.CallFaker.Generate(10); 
        });

        public static Faker<BAddress> AddressFaker = new Faker<BAddress>("es")
        .Rules((f, a) =>
        {
            a.Country = f.Address.Country();
            a.Locality = f.Address.City();
            a.Province = f.Address.State();
        });

        public static Faker<Call> CallFaker = new Faker<Call>()
        .Rules((f, c) =>
        {
            c.Duration = f.Random.Int(1, 20);
            c.StartTime = f.Date.Future();
            c.Transmitter = ModelFakers.ClientFaker.Generate(1)[0];
            c.Receiver = ModelFakers.ClientFaker.Generate(1)[0];
        });

    }
}