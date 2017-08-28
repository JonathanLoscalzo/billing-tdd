using System;

namespace Billing.Entities.Models
{
    public class Call : Guid
    {
        public Client Transmitter { get; set; }

        public Client Receiver { get; set; }

        /// momento en que comenzó la llamada
        public DateTime StartTime { get; set; }

        /// Duración en minutos
        public int Duration { get; set; }
    }
}