using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.Execution
{
    public class TourExecutionDto
    {
        public long Id { get; set; }
        public long TourId { get; set; }
        public long UserId { get; set; }
        public double TourRange { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime LastActivity { get; set; }
        public ExecutionStatus ExecutionStatus { get; set; }
        public ICollection<CheckPointStatusDto> CheckpointsStatus { get; set; }
    }

    public enum ExecutionStatus
    {
        Active,
        Completed,
        Abandoned
    }
}

