using BAddress = Billing.Entities.Models.Address;
using Billing.Business.Services;
using Billing.Business.Contracts;

using Xunit;
using Billing.Data.Repositories;
using Moq;
using Billing.Data.Contracts;
using System;

namespace Billing.Test.Business
{
    [Trait("Category", "Business")]
    public class CostExternalServicesTest
    {
        private readonly CostRepository costRepository;
        private readonly ICostExternalService costExternalService;

        public CostExternalServicesTest()
        {
            var mockRepo = new AddressRepository(); // Deberia mockear al ser UT, si fuera integración no.
            this.costRepository = new CostRepository(mockRepo);
            this.costExternalService = new CostExternalService(this.costRepository);
        }

        [Fact]
        public void GetCostFromNationalCall()
        {
            foreach (var address in this.costRepository.NationalCost)
            {
                Assert.Equal(address.Value, this.costExternalService.GetCostFromNationalCall(address.Key));
            }
        }

        [Fact]
        public void GetCostFromInternationalCall()
        {
            foreach (var address in this.costRepository.InternationalCost)
            {
                Assert.Equal(address.Value, this.costExternalService.GetCostFromInternationalCall(new BAddress() { Country = address.Key }));
            }
        }
    }
}