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
        public TimeSpan Time{ get; private set; }
        public TransportType TransportType { get; private set; }

        [JsonConstructor]
        public TransportDuration(TimeSpan time, TransportType transportType)
        {
            this.Time=time;
            this.TransportType = transportType;
            Validate();
        }

        private void Validate()
        {
            if (Time < TimeSpan.Zero)
                throw new ArgumentException("Time duration cannot be less than zero.");
            if (!Enum.IsDefined(typeof(TransportType), TransportType))
                throw new ArgumentException("Invalid transport type value.");
        }
        /*
        public static double CalculateTime(double distanceInKm, TransportType transportType)
        {
            double speed;

            switch (transportType)
            {
                case TransportType.Car:
                    speed = 60; 
                    break;
                case TransportType.Bicycle:
                    speed = 15;
                    break;
                case TransportType.Walking:
                    speed = 5;
                    break;
                default:
                    throw new ArgumentException("Invalid transport type");
            }

            return distanceInKm / speed;
        }*/

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
    }
    public enum TransportType
    {
        Car,
        Bicycle,
        Walking
    }
}
