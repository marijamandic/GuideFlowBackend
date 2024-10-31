using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.Tours
{
    public class Tour: Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Level Level { get; set; }
        public TourStatus Status { get; set; }
        public double LenghtInKm { get; set; }       
        public Price Price { get; set; }
        public List<String> Taggs { get; set; }
        public List<Checkpoint> Checkpoints { get; set; }
        public List<Duration> Durations { get; set; }



        public Tour()
        {
            this.Status = TourStatus.Draft;
        }
        public Tour(string name, string? description, Level level, List<String> taggs,List<Checkpoint> checkpoints)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Invalid Name.");

            Name = name;
            Description = description;
            Level = level;
            Taggs = taggs;
            this.Status = TourStatus.Draft;
            Price = new Price();
            Checkpoints = checkpoints;
            Durations = new List<Duration>();
        }

        public void Validate()
        {
            //TODO
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
