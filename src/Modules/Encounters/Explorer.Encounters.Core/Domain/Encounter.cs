using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain
{
    public class Encounter : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public EncounterLocation EncounterLocation { get; private set; }
        public EncounterStatus EncounterStatus { get; private set; }
        public int ExperiencePoints { get; private set; }
        public EncounterType EncounterType { get; private set; }
        public Encounter() { }
        public Encounter(string name, string description, EncounterLocation location, EncounterStatus status, int experiencePoints , EncounterType encounterType)
        {
            Name = name;
            Description = description;
            EncounterLocation = location;
            EncounterStatus = status;
            ExperiencePoints = experiencePoints;
            EncounterType = encounterType;
            Validate();
        }
        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new ArgumentException("Name cannot be null or empty.");

            if (Name.Length > 100)
                throw new ArgumentException("Name cannot exceed 100 characters.");

            if (string.IsNullOrWhiteSpace(Description))
                throw new ArgumentException("Description cannot be null or empty.");

            if (Description.Length > 500)
                throw new ArgumentException("Description cannot exceed 500 characters.");

            if (ExperiencePoints < 0)
                throw new ArgumentException("ExperiencePoints cannot be negative.");

            if (!Enum.IsDefined(typeof(EncounterStatus), EncounterStatus))
                throw new ArgumentException("Invalid Status value.");
            if (!Enum.IsDefined(typeof(EncounterType), EncounterType))
                throw new ArgumentException("Invalid type value.");
        }
    }
    public enum EncounterStatus {
        Active = 0,
        Draft = 1,
        Archieved = 2
    }
    public enum EncounterType { 
        Social = 0,
        Location = 1,
        Misc = 2
    }
}