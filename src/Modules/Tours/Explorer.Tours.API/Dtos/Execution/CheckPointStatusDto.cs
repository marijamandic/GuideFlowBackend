using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.Execution
{
    public class CheckPointStatusDto
    {
        public long CheckpointId { get;  set; }
        public long TourExecutionId { get;  set; }
        public double Latitude { get;  set; }
        public double Longitude { get;  set; }
        public DateTime CompletionTime { get;  set; }
    }
}
