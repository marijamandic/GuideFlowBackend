using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Explorer.Tours.Core.Domain.Tours
{
    public class Checkpoint: Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public string? ImageUrl { get; private set; }
        public string Secret {  get; private set; }

        public Checkpoint(string name, string description, double latitude, double longitude, string? imageUrl,string secret)
        {
            Name = name;
            Description = description;
            Latitude = latitude;
            Longitude = longitude;
            ImageUrl = imageUrl;
            Secret = secret;
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Name.");
            if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid Description.");
            if (Latitude < -90 || Latitude > 90) throw new ArgumentException("Invalid Latitude value.");
            if (Longitude < -180 || Longitude > 180) throw new ArgumentException("Invalid Longitude value.");
            if (string.IsNullOrWhiteSpace(Secret)) throw new ArgumentException("Invalid Secret.");
        }

        public void Update(Checkpoint updatedCheckpoint)
        {
            Name = updatedCheckpoint.Name;
            Description = updatedCheckpoint.Description;
            Latitude = updatedCheckpoint.Latitude;
            Longitude = updatedCheckpoint.Longitude;
            ImageUrl = updatedCheckpoint.ImageUrl;
            Secret = updatedCheckpoint.Secret;
        }
    }

}

