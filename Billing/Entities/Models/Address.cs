namespace Billing.Entities.Models
{
    public class Address : Guid
    {
        public string Locality { get; set; }

        public string Province { get; set; }

        public string Country { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (Address)obj;

            return this.Locality == other.Locality
                && this.Province == other.Province
                && this.Country == other.Country;
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}