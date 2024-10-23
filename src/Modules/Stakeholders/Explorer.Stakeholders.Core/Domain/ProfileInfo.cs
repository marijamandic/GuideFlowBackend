using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class ProfileInfo : Entity
    {
        public long UserId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string ImageUrl { get; private set; }
        public string Biography { get; private set; }
        public string Moto { get; private set; }

        public ProfileInfo(long userId, string firstName, string lastName, string imageUrl, string biography, string moto)
        {
            Validate(firstName, lastName, imageUrl, biography, moto);

            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            ImageUrl = imageUrl;
            Biography = biography;
            Moto = moto;
        }

        private void Validate(string firstName, string lastName, string imageUrl, string biography, string moto)
        {
            if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name cannot be empty.", nameof(firstName));
            if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("Last name cannot be empty.", nameof(lastName));
            if (string.IsNullOrWhiteSpace(imageUrl)) throw new ArgumentException("ImageUrl cannot be empty.", nameof(imageUrl));
            if (string.IsNullOrWhiteSpace(biography)) throw new ArgumentException("Biography cannot be empty.", nameof(biography));
            if (string.IsNullOrWhiteSpace(moto)) throw new ArgumentException("Moto cannot be empty.", nameof(moto));
        }
    }
}
