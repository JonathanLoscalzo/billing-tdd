namespace Billing.Entities.Models
{
    public class AddressTax : Guid
    {
        public Address Address { get; set; }
        public double Tax { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return this.Address.Equals(((AddressTax)obj).Address);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}