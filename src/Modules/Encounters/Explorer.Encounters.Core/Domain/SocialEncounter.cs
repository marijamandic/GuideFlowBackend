using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain
{
    public class SocialEncounter : Encounter
    {
        public int TouristNumber {  get; private set; }
        public double EncounterRange { get; private set; }

        public SocialEncounter() { }
        public SocialEncounter(string name, string description, EncounterLocation location, EncounterStatus status, int experiencePoints, EncounterType encounterType ,int touristNumber, double encounterRange) : base(name, description, location, status, experiencePoints, encounterType) { 
            TouristNumber = touristNumber;
            EncounterRange = encounterRange;
            Validate();
        }
        private void Validate() {
            if (TouristNumber < 0)
                throw new ArgumentException("Tourist number cannot be negative.");
            if (EncounterRange < 0)
                throw new ArgumentException("Range cannot be negative.");
        }
    }
}
