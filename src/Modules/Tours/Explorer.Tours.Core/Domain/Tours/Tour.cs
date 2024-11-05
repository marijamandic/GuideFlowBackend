using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
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
        public double LengthInKm { get; private set; }
        public Price Price { get; private set; }
        public double AverageGrade { get; private set; }  
        public List<string> Taggs { get; private set; }
        public List<Checkpoint> Checkpoints { get; private set; }
        public List<TransportDuration> TransportDurations { get; private set; }
        public List<TourReview> Reviews { get; private set; }

        public Tour(string name,long authorId, string description, Level level,double lengthInKm,Price price,double averageGrade, TourStatus status = TourStatus.Draft)
        {
            Name = name;
            AuthorId = authorId;
            Description = description;
            Level = level;
            Status = status;
            LengthInKm = lengthInKm; 
            Price = price;
            AverageGrade = averageGrade;
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

        public void ChangeStatusToPublish()
        {
            Status = TourStatus.Published;
        }
    }

    public enum TourStatus
    {
        Draft,
        Published,
        Archived
    }

    public enum Level 
    {
        Easy,
        Advanced,
        Expert
    }

  

}
