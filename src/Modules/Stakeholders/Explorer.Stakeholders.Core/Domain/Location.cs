using System;
using System.Text.Json.Serialization;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain
{
    public class Location : ValueObject<Location>
    {
        public double Longitude { get; private set; }
        public double Latitude { get; private set; }

        public Location() { }
        [JsonConstructor]
        public Location(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        protected override bool EqualsCore(Location other)
        {
            return Longitude == other.Longitude && Latitude == other.Latitude;
        }

        protected override int GetHashCodeCore()
        {
            return HashCode.Combine(Longitude, Latitude);
        }
    }
}
