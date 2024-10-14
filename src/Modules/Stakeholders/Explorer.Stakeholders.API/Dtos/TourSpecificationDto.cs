using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Explorer.Stakeholders.API.Dtos
{
    public enum TransportationMode { WALK, BIKE, CAR, BOAT }

    public class TourSpecificationDto
    {
        public long UserId { get; set; }
        public int TourDifficulty { get; set; }
        public Dictionary<TransportationMode, int> TransportRatings { get; set; }
        public List<string> Tags { get; set; }

        public TourSpecificationDto()
        {
            TransportRatings = new Dictionary<TransportationMode, int>();
            Tags = new List<string>();
        }
    }
}
