using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain
{
    public class EncounterLocation : ValueObject<EncounterLocation>
    {
        public double Latitude { get; private set; }
        public double Longitude { get; private set;}

        public EncounterLocation(double latitude , double longitude) {
            Latitude = latitude;
            Longitude = longitude;
            Validate();
        }
        private void Validate() {
            if (Latitude < -90 || Latitude > 90) throw new ArgumentException("Invalid Latitude value.");
            if (Longitude < -180 || Longitude > 180) throw new ArgumentException("Invalid Longitude value.");
        }
        protected override bool EqualsCore(EncounterLocation other)
        {
            return Latitude == other.Latitude &&
                    Longitude == other.Longitude;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = Latitude.GetHashCode();
                hashCode = (hashCode * 397) ^ Longitude.GetHashCode();

                return hashCode;
            }
        }
    }
}
