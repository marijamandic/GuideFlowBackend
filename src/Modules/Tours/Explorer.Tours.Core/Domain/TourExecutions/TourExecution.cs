using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.TourExecutions
{
    public class TourExecution : Entity
    {
        public long TourId { get; private set; }
        public long UserId { get; private set; }
        public double TourRange {  get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public DateTime LastActivity {  get; private set; }
        public ExecutionStatus ExecutionStatus { get; private set; }
        public ICollection<CheckpointStatus> CheckpointsStatus { get; private set; } = new List<CheckpointStatus>();
        public TourExecution(long tourId, long userId, double tourRange)
        {
            TourId = tourId;
            UserId = userId;
            TourRange = tourRange;
            StartTime = DateTime.UtcNow;
            LastActivity = DateTime.UtcNow;
            ExecutionStatus = ExecutionStatus.Active;
        }
        public void UpdateLocation(double longitude , double latitude) {
            foreach (var checkpointStatus in CheckpointsStatus)
            {
                if (!checkpointStatus.IsCompleted() && checkpointStatus.IsTouristNear(latitude, longitude))
                {
                    checkpointStatus.MarkAsCompleted(); 
                }
            }

        }

        public void AddCheckpointStatuses(List<Checkpoint> checkpoints) { 
            foreach( Checkpoint checkpoint in checkpoints)
            {
                CheckpointsStatus.Add(new CheckpointStatus(checkpoint.Id,checkpoint.Latitude,checkpoint.Longitude));
            }
        }
    }
}

public enum ExecutionStatus { 
    Active,
    Completed,
    Abandoned
}
