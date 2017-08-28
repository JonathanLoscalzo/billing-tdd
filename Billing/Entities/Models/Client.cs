using System.Collections.Generic;

namespace Billing.Entities.Models
{
    public class Client : Guid
    {
        /// nro que lo identifica
        public int Profile { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }
        // public string FullName
        // {
        //     get => string.Format("{0} {1}", this.Name, this.FullName);
        // }

        /// Abono mensual
        public double MontlyPrice { get; set; }

        public int PhoneNumber { get; set; }

        public Address Address { get; set; }

        public IList<Call> Calls { get; set; }

        public Client()
        {
            this.Calls = new List<Call>();
        }
    }
}