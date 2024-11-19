using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourSpecificationDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public Level Level { get; set; }
        public List<string> Taggs { get; set; }
        public List<TransportRatingDto> TransportRatings { get; set; } = new List<TransportRatingDto>();
    }
}
