namespace Billing.Entities.Models
{
    public class Guid
    {
        public Guid()
        {
            this.Id = this.GetHashCode();
        }

        public int Id { get; set; }

        public object Clone() => this.MemberwiseClone();
    }
}