using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
        public enum TransportationMode { WALK, BIKE, CAR, BOAT }

    public class TourSpecificationDto
    {
        public long UserId { get; set; }
        public int TourDifficulty { get; set; }
        public int WalkRating { get; set; }
        public int BikeRating { get; set; }
        public int CarRating { get; set; }
        public int BoatRating { get; set; }
        public List<string> Tags { get; set; }

        public TourSpecificationDto()
        {
            Tags = new List<string>();
        }
    }

}
