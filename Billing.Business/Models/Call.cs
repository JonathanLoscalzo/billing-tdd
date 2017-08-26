using System;
using Billing.Business.Helpers;
using Billing.Business.Models.CostStrategies;

namespace Billing.Business.Models
{
    public class Call
    {
        public Client Transmitter { get; set; }

        public Client Receiver { get; set; }

        /// momento en que comenzó la llamada
        public DateTime StartTime { get; set; }

        /// Duración en segundos
        public int Duration { get; set; }

        /// Cuando se setea el tipo de la llamada, se setea la estrategia.
        public Calls CallType
        {
            get => this.CallType;
            set => this.CallStrategy = ICallType.Factory(value);
        }

        public ICallType CallStrategy { get; private set; }

        public double Cost() => this.CallStrategy.HowMuchCost(this);
    }
}