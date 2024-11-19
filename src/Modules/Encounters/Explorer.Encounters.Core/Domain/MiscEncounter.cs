using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain
{
    public class MiscEncounter : Encounter
    {
        public string ActionDescription { get; private set; }

        public MiscEncounter() { }
        public MiscEncounter(string name, string description, EncounterLocation location, EncounterStatus status, int experiencePoints,EncounterType encounterType ,string actionDescription) : base(name, description, location, status, experiencePoints,encounterType)
        {
            ActionDescription = actionDescription;
            Validate();
        }
        private void Validate() {
            if (string.IsNullOrWhiteSpace(Description))
                throw new ArgumentException("Description cannot be null or empty.");
        }
    }
}
