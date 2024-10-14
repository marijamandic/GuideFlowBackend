using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public enum TransportationMode { WALK, BIKE, CAR, BOAT}
    public class TourSpecifications : Entity
    {
        public long UserId { get; set; }
        public int TourDifficulty {  get; set; }
        public Dictionary<TransportationMode, int> TransportRatings { get; set; }
        public List<string> Tags { get; set; }

        public TourSpecifications(long userId, int tourDifficulty, Dictionary<TransportationMode, int> transportRatings, List<string> tags)
        {
            UserId = userId;
            TourDifficulty = tourDifficulty;
            TransportRatings = transportRatings;
            Tags = tags;
        }

        public TourSpecifications()
        {
            TransportRatings = new Dictionary<TransportationMode, int>();
            Tags = new List<string>();
        }

        private void Validate()
        {
            if (UserId == 0) throw new ArgumentException("Invalid UserId");
            if (TourDifficulty < 1 || TourDifficulty > 5) throw new ArgumentException("Tour Difficulty must be in range of 1 to 5");
            foreach(var rating in TransportRatings.Values)
            {
                if (rating < 0 || rating > 3) throw new ArgumentException("Transport rating must be in range of 1 to 3");
            }
            foreach (var tag in Tags)
            {
                if (string.IsNullOrWhiteSpace(tag)) throw new ArgumentException("Tags must not be null");
            }
        }
    }
}
