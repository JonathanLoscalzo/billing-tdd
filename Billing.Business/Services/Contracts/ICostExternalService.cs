using Billing.Business.Models;

namespace Billing.Business.Services.Contracts
{
    public interface ICostExternalService
    {
        ///  Las llamadas Nacionales tienen un costo distinto según la localidad a la que se llame
        double GetCostFromNationalCall(Address address);

        ///  Las llamadas Internacionales tienen un costo distinto según el país al que se llame
        double GetCostFromInternationalCall(Address address);
    }
}