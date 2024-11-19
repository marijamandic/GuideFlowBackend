using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TransportRatingDto
    {
        public int Rating { get; set; }
        public TransportMode TransportMode { get; set; }

        public TransportRatingDto() { }

        public TransportRatingDto(TransportMode transportMode, int rating)
        {
            TransportMode = transportMode;
            Rating = rating;
        }
    }
}

public enum TransportMode
{
    Walk,
    Bike,
    Car,
    Boat
}
