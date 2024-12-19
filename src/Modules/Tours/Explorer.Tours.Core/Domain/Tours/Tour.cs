    using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.Tours
{
    public class Tour : Entity
    {
        public string Name { get; private set; }
        public long AuthorId { get; private set; }
        public string Description { get; private set; }
        public Level Level { get; private set; }
        public TourStatus Status { get; private set; }
        public DateTime? StatusChangeDate { get; private set; }
        public double LengthInKm { get; private set; }
        public int Price { get; private set; }
        public double AverageGrade { get; private set; }  
        public List<string> Taggs { get; private set; }
        public WeatherCondition WeatherRequirements { get; private set; }
        public List<Checkpoint> Checkpoints { get; private set; }
        public List<TransportDuration> TransportDurations { get; private set; }
        public List<TourReview> Reviews { get; private set; }

        public Tour(string name,long authorId, string description, Level level,double lengthInKm,int price,double averageGrade,WeatherCondition weatherRequirements, TourStatus status = TourStatus.Draft)
        {
            Name = name;
            AuthorId = authorId;
            Description = description;
            Level = level;
            Status = status;
            LengthInKm = lengthInKm; 
            Price = price;
            AverageGrade = averageGrade;
            WeatherRequirements = weatherRequirements;
            Taggs = new List<string>();
            Checkpoints = new List<Checkpoint>();
            TransportDurations = new List<TransportDuration>();
            Reviews = new List<TourReview>();
            Validate();
        }

        private void Validate()
        {
            if (AuthorId == 0) throw new ArgumentException("Invalid Author Id");

            if (string.IsNullOrWhiteSpace(Name))
                throw new ArgumentException("Name cannot be null or empty.");

            if (string.IsNullOrWhiteSpace(Description))
                throw new ArgumentException("Description cannot be null or empty.");

            if (LengthInKm < 0)
                throw new ArgumentException("Length in kilometers cannot be less than 0.");

            if (!Enum.IsDefined(typeof(Level), Level))
                throw new ArgumentException("Invalid level value.");

        }

        public void AddCheckpoint(Checkpoint checkpoint)
        {
            Checkpoints.Add(checkpoint);
        }

        public void AddTransportDuratios(List<TransportDuration> transportDurations)
        {
            TransportDurations = transportDurations;
        }


        public void Archive()
        {
            if (Status != TourStatus.Published)
                throw new InvalidOperationException("Only published tours can be archived.");

            Status = TourStatus.Archived;
            StatusChangeDate = DateTime.UtcNow;
        }

        public bool CheckPublishConditions()
        {
            if (AuthorId == 0) return false;
            if (string.IsNullOrWhiteSpace(Name)) return false;
            if (string.IsNullOrWhiteSpace(Description)) return false;
            if (!Enum.IsDefined(typeof(Level), Level)) return false;
            if (Taggs == null || !Taggs.Any()) return false;
            if (Checkpoints == null || Checkpoints.Count < 2) return false;
            if (TransportDurations == null || !TransportDurations.Any()) return false;
            return true;
        }
        public void ChangeStatusToPublish()
        {
            Status = TourStatus.Published;
            StatusChangeDate = DateTime.UtcNow;
        }

        public void UpdateLength(double length)
        {
            if (length < 0)
                throw new ArgumentException("Length in kilometers cannot be less than 0.");
            else
                LengthInKm =length;
        }

        public void UpdateCheckpoint(Checkpoint updatedCheckpoint)
        {
            Checkpoint? oldCheckpoint = Checkpoints.Find(ch => ch.Id == updatedCheckpoint.Id);
            if (oldCheckpoint == null)
            {
                throw new KeyNotFoundException($"Checkpoint with ID {updatedCheckpoint.Id} not found.");
            }
            oldCheckpoint.Update(updatedCheckpoint);
        }

        public void DeleteCheckpoint(Checkpoint deletedCheckpoint)
        {
            Checkpoint? checkpoint = Checkpoints.Find(ch => ch.Id == deletedCheckpoint.Id);
            if (checkpoint == null)
            {
                throw new KeyNotFoundException($"Checkpoint with ID {deletedCheckpoint.Id} not found.");
            }
            Checkpoints.Remove(checkpoint);
        }
    }

    public enum TourStatus
    {
        Draft,
        Published,
        Archived,
        Deleted
    }

    public enum Level 
    {
        Easy,
        Advanced,
        Expert
    }

  

}
