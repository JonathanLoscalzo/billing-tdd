using System;
using Calls = Billing.Entities.Enums.Calls;
using Billing.Data.Helpers;
using Billing.Data.CostStrategies;

namespace Billing.Entities.Models
{
    public class Call
    {
        public Client Transmitter { get; set; }

        public Client Receiver { get; set; }

        /// momento en que comenzó la llamada
        public DateTime StartTime { get; set; }

        /// Duración en minutos
        public int Duration { get; set; }

        /// Cuando se setea el tipo de la llamada, se setea la estrategia.
        public Calls CallType
        {
            get => Transmitter.GetCallType(Receiver);
        }

        public DestinationCall DestionationCall
        {
            get => DestinationCall.GetInstance(this.CallType);
        }

        public double Cost() => this.DestionationCall.HowMuchCost(this);
    }
}