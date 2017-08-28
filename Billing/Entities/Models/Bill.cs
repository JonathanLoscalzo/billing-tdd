using Billing.Entities.Enums;

namespace Billing.Entities.Models
{
    public class Bill : Guid
    {
        /// nrocliente - fullname
        public string Client { get; set; }

        /// Abono mensual
        public double MontlyPrice { get; set; }

        /// 1 a 12
        public Months Month { get; set; }

        public int Year { get; set; }

        public double NationalCallCost { get; set; }

        public double InternationalCallCost { get; set; }

        public double LocalCallCost { get; set; }

        public double TotalCost
        {
            get
            {
                return this.InternationalCallCost
                + this.LocalCallCost
                + this.NationalCallCost
                + this.MontlyPrice;
            }
        }
    }
}