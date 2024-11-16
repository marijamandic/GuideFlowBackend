using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos
{
    public class EncounterDto
    {
        public long Id {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EncounterLocationDto EncounterLocationDto { get; set; }
        public EncounterStatus EncounterStatus { get; set; }
        public int ExperiencePoints { get; set; }
    }
    public enum EncounterStatus
    {
        Active,
        Draft,
        Archieved
    }
}