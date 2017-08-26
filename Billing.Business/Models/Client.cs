using System.Collections.Generic;
using System.Linq;
using Billing.Business.Helpers;
using CallsTypes = Billing.Business.Helpers.Calls;

namespace Billing.Business.Models
{
    public class Client
    {
        /// nro que lo identifica
        public int Profile { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return $"{this.Name} {this.FullName}";
            }
        }

        /// Abono mensual
        public double MontlyPrice { get; set; }

        public int PhoneNumber { get; set; }

        public Address Address { get; set; }

        public IEnumerable<Call> Calls { get; set; }

        public double GetNationalCost() => this.GetCostFrom(CallsTypes.National);

        public double GetInternationalCost() => this.GetCostFrom(CallsTypes.International);

        public double GetLocalCost() => this.GetCostFrom(CallsTypes.Local);

        private double GetCostFrom(Calls callType)
        {
            return this.Calls
            .Where(c => c.CallType == callType)
            .Sum(v => v.Cost());
        }
    }
}