using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.TourExecutions
{
    public class CheckpointStatus : Entity
    {
        public long CheckpointId { get; private set; }
        public long TourExecutionId { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public DateTime CompletionTime { get; private set; } = DateTime.MinValue;
        public CheckpointStatus(long checkpointId, double latitude, double longitude)
        {
            CheckpointId = checkpointId;
            Latitude = latitude;
            Longitude = longitude;
        }
        public bool IsCompleted() {
            return CompletionTime != DateTime.MinValue;
        }
    }
}
