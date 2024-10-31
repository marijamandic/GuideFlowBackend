using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.Tours
{
    public class Duration : ValueObject<Duration>
    {
        public double Time;

        public TransportType TransportType;

        public Duration()
        {
        }

        public Duration(double km, TransportType transportType)
        {
            this.TransportType = transportType;
            this.Time = CalculateTime(km,transportType);
        }

        public static double CalculateTime(double distanceInKm, TransportType transportType)
        {
            double speed; // u km/h

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

           
            return distanceInKm / speed; // rezultat je u satima
        }

        public void Validate()
        {
            //TODO
        }


        protected override bool EqualsCore(Duration other)
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
