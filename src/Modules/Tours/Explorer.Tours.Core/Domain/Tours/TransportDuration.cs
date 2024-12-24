using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.Tours
{
    public class TransportDuration : ValueObject<TransportDuration>
    {
        public int Time{ get; private set; }
        public TransportType TransportType { get; private set; }

        [JsonConstructor]
        public TransportDuration(int time, TransportType transportType)
        {
            this.Time=time;
            this.TransportType = transportType;
            Validate();
        }

        private void Validate()
        {
            if (Time < 0)
                throw new ArgumentException("Time duration cannot be less than zero.");
            if (!Enum.IsDefined(typeof(TransportType), TransportType))
                throw new ArgumentException("Invalid transport type value.");
        }

        protected override bool EqualsCore(TransportDuration other)
        {
            return Time == other.Time &&
                    TransportType == other.TransportType;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = Time.GetHashCode();
                hashCode = (hashCode * 397) ^ TransportType.GetHashCode();

                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"{TransportType} ({Time} minutes)";
        }

    }
    public enum TransportType
    {
        Car,
        Bicycle,
        Walking
    }
}
