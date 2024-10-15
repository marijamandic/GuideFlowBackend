using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;


namespace Explorer.Tours.Core.Domain
{
    public enum TransportationMode { WALK, BIKE, CAR, BOAT }
    public class TourSpecifications : Entity
    {
        public long UserId { get; set; }
        public int TourDifficulty { get; set; }
        public int WalkRating { get; set; }
        public int BikeRating { get; set; }
        public int CarRating { get; set; }
        public int BoatRating { get; set; }
        public List<string> Tags { get; set; }

        public TourSpecifications(long userId, int tourDifficulty, int walkRating, int bikeRating, int carRating, int boatRating, List<string> tags)
        {
            UserId = userId;
            TourDifficulty = tourDifficulty;
            WalkRating = walkRating;
            CarRating = carRating;
            BikeRating = bikeRating;
            BoatRating = boatRating;
            Tags = tags;
        }

        public TourSpecifications()
        {
            Tags = new List<string>();
        }

        private void Validate()
        {
            if (UserId == 0) throw new ArgumentException("Invalid UserId");
            if (TourDifficulty < 1 || TourDifficulty > 5) throw new ArgumentException("Tour Difficulty must be in range of 1 to 5");
            if (WalkRating < 0 || WalkRating > 3) throw new ArgumentException("Transport rating must be in range of 1 to 3");
            if (CarRating < 0 || CarRating > 3) throw new ArgumentException("Transport rating must be in range of 1 to 3");
            if (BikeRating < 0 || BikeRating > 3) throw new ArgumentException("Transport rating must be in range of 1 to 3");
            if (BoatRating < 0 || BoatRating > 3) throw new ArgumentException("Transport rating must be in range of 1 to 3");
            foreach (var tag in Tags)
            {
                if (string.IsNullOrWhiteSpace(tag)) throw new ArgumentException("Tags must not be null");
            }
        }
    }

}
