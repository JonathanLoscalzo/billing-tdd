using Billing.Business.Contracts;
using Billing.Business.Services;
using Billing.Data.Contracts;
using Billing.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Billing.Linker
{
    public static class Installers
    {
        public static void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddTransient<IBillingService, BillingService>()
                .AddTransient<ICallService, CallService>()
                .AddTransient<IClientService, ClientService>()
                .AddTransient<ICostExternalService, CostExternalService>();

            serviceCollection
                .AddSingleton<IBillRepository, BillingRepository>()
                .AddSingleton<ICallRepository, CallRepository>()
                .AddSingleton<IClientRepository, ClientRepository>()
                .AddSingleton<ICostRepository, CostRepository>()
                .AddSingleton<IAddressRepository, AddressRepository>();
        }
    }
}