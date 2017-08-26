using Billing.Business.Models;

namespace Billing.Business.Helpers
{
    public static class ClientAddressExtensions
    {
        /// Mismo pais
        public static bool IsNationalCall(this Client from, Client to) =>
            from.Address.Country == to.Address.Country
            && from.Address.Locality != to.Address.Locality
            && from.Address.Province != to.Address.Province;

        /// Distinto pais
        public static bool IsInternacionalCall(this Client from, Client to) => from.Address.Country != to.Address.Country;

        /// Misma Localidad y Provincia
        public static bool IsLocalCall(this Client from, Client to) => from.Address.Equals(to.Address);

        public static Calls GetCallType(this Client from, Client to)
        {
            if (from.IsLocalCall(to))
            {
                return Calls.Local;
            }
            else if (from.IsNationalCall(to))
            {
                return Calls.National;
            }
            else if (from.IsInternacionalCall(to))
            {
                return Calls.International;
            }

            return Calls.Empty;
        }
    }
}