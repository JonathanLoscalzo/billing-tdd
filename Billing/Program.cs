using Microsoft.Extensions.DependencyInjection;
using Billing.Linker;

namespace Billing
{
    public class Program
    {
        public static ServiceProvider serviceProvider { get; set; }

        public static void Main(string[] args)
        {
            //setup our DI
            serviceProvider = StartUp.ConfigureServices();
        }
    }
}