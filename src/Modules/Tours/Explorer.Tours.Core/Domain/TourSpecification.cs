using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain.TourExecutions;

namespace Explorer.Tours.Core.Domain
{
    public class TourSpecification : Entity
    {
        public long UserId { get; private set; }
        public Level Level { get; private set; }
        public List<string> Taggs { get; private set; }
        public ICollection<TransportRating> TransportRatings { get; private set; } = new List<TransportRating>();

        public TourSpecification()
        {
            Taggs = new List<string>();
            TransportRatings = new List<TransportRating>();
        }

        public TourSpecification(long userId, Level level, List<string> taggs)
        {
            UserId = userId;
            Level = level;
            Taggs = taggs;
            TransportRatings = new List<TransportRating>();
        }
    }

}
