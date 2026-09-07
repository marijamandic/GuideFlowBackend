using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.Execution
{
    public class CheckPointStatusDto
    {
        public long Id {  get; set; }
        public long CheckpointId { get;  set; }
        public long TourExecutionId { get;  set; }
        public CheckpointDto? Checkpoint { get; set; }
        public DateTime CompletionTime { get;  set; }
    }
}
