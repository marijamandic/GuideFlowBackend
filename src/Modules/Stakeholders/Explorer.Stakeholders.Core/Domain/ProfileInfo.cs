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
        public string ProfilePicture { get; private set; }
        public string Biography { get; private set; }
        public string Moto { get; private set; }

        public ProfileInfo(long userId, string firstName, string lastName, string profilePicture, string biography, string moto)
        {
            Validate(firstName, lastName, profilePicture, biography, moto);

            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            ProfilePicture = profilePicture;
            Biography = biography;
            Moto = moto;
        }

        private void Validate(string firstName, string lastName, string profilePicture, string biography, string moto)
        {
            if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name cannot be empty.", nameof(firstName));
            if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("Last name cannot be empty.", nameof(lastName));
            if (string.IsNullOrWhiteSpace(profilePicture)) throw new ArgumentException("Profile picture cannot be empty.", nameof(profilePicture));
            if (string.IsNullOrWhiteSpace(biography)) throw new ArgumentException("Biography cannot be empty.", nameof(biography));
            if (string.IsNullOrWhiteSpace(moto)) throw new ArgumentException("Moto cannot be empty.", nameof(moto));
        }
    }
}
