using Billing.Linker;
using Microsoft.Extensions.DependencyInjection;

namespace Billing.Test.Common
{
    public class TestStartup : StartUp
    {
        protected ServiceProvider container { get; set; }

        public TestStartup() => this.ConfigureTestServices(new ServiceCollection());

        public ServiceProvider ConfigureTestServices(ServiceCollection services)
        {
            return this.container = StartUp.ConfigureServices(services);
        }
    }
}