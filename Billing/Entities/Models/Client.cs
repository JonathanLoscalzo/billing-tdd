using System.Collections.Generic;
using System.Linq;
using CallsEnum = Billing.Entities.Enums.Calls;

namespace Billing.Entities.Models
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

        public double GetNationalCost() => this.GetCostFrom(CallsEnum.National);

        public double GetInternationalCost() => this.GetCostFrom(CallsEnum.International);

        public double GetLocalCost() => this.GetCostFrom(CallsEnum.Local);

        private double GetCostFrom(CallsEnum callType)
        {
            return this.Calls
            .Where(c => c.CallType == callType)
            .Sum(v => v.Cost());
        }
    }
}