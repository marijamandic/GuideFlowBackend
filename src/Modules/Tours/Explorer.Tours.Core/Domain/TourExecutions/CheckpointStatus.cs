using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;
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
        public Checkpoint Checkpoint { get; private set; }
        public long TourExecutionId { get; private set; }
        public DateTime CompletionTime { get; private set; } = DateTime.MinValue;
        public CheckpointStatus(long checkpointId){
            CheckpointId = checkpointId;
        }
        public bool IsCompleted() {
            return CompletionTime != DateTime.MinValue;
        }
        public bool IsTouristNear(double latitude, double longitude)
        {
            const double tolerance = 0.0018; // Tolerancija za blizinu (oko 11 metara)

            bool isNearLatitude = Math.Abs(Checkpoint.Latitude - latitude) <= tolerance;
            bool isNearLongitude = Math.Abs(Checkpoint.Longitude - longitude) <= tolerance;

            return isNearLatitude && isNearLongitude;
        }
        public void MarkAsCompleted() {
            CompletionTime = DateTime.UtcNow;
        }

    }
}
