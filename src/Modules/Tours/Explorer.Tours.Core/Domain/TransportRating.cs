using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class TransportRating : Entity
    {
        public long TourSpecificationId { get; private set; }
        public TourSpecification TourSpecification { get; private set; }
        public int Rating { get; private set; }
        public TransportMode TransportationMode { get; private set; }

        public TransportRating(long tourSpecificationId, int rating, TransportMode transportationMode)
        {
            TourSpecificationId = tourSpecificationId;
            Rating = rating;
            TransportationMode = transportationMode;
        }

        public TransportRating() { }

        public void Validate()
        {
            if (TourSpecificationId == 0)
                throw new ArgumentException("Invalid Tour specification Id");
            if (Rating < 0 || Rating > 3)
                throw new ArgumentException("Rate must be between 0 and 3");
        }

    }

    public enum TransportMode
    {
        Car,
        Bicycle,
        Walking,
        Boat
    }
}
