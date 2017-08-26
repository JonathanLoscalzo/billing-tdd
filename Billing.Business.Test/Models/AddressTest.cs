using Xunit;
using Bogus.DataSets;
using Bogus.Extensions;
using Bogus;
using AddressModel = Billing.Business.Models.Address;

namespace Billing.Business.Test.Models
{
    public class AddressTest
    {
        [Fact]
        public void EqualsReturnsTrue()
        {
            var faker = new Faker("es");

            var Locality = faker.Address.City();
            var Country = faker.Address.Country();
            var Province = faker.Address.State();

            var addressModel1 = new AddressModel()
            {
                Locality = Locality,
                Province = Province,
                Country = Country,
            };

            var addressModel2 = new AddressModel()
            {
                Locality = Locality,
                Province = Province,
                Country = Country,
            };

            Assert.True(addressModel1.Equals(addressModel2), " Instancias distintas, pero debería dar igual por datos");
            Assert.True(addressModel1.Equals(addressModel2), " Instancias distintas, pero debería dar igual por datos");
            Assert.True(addressModel2.Equals(addressModel1), " Instancias distintas, pero debería dar igual por datos");
            Assert.True(addressModel1.Equals(addressModel1), " Mismas Instancias, deberían dar igual");
            Assert.True(addressModel2.Equals(addressModel2), " Mismas Instancias, deberían dar igual");
        }

        [Fact]
        public void EqualsReturnsFalse()
        {
            var faker = new Faker("es");

            var Locality = faker.Address.City();
            var Country = faker.Address.Country();
            var Province = faker.Address.State();
            
            var addressModel1 = new AddressModel()
            {
                Locality = Locality,
                Province = Province,
                Country = Country,
            };

            var addressModel2 = new AddressModel()
            {
                Locality = Locality + "DIFF",
                Province = Province + "DIFF",
                Country = Country + "DIFF",
            };

            Assert.False(addressModel1.Equals(addressModel2), " Instancias distintas, pero debería dar igual por datos");
            Assert.False(addressModel2.Equals(addressModel1), " Instancias distintas, pero debería dar igual por datos");
        }
    }
}