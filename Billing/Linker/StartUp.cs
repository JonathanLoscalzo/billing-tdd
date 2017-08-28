using Microsoft.Extensions.DependencyInjection;

namespace Billing.Linker
{
    public class StartUp
    {
        public static ServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();
            return StartUp.ConfigureServices(serviceCollection);
        }

        public static ServiceProvider ConfigureServices(IServiceCollection serviceCollection)
        {
            Installers.Configure(serviceCollection);
            return serviceCollection.BuildServiceProvider();
        }
    }
}