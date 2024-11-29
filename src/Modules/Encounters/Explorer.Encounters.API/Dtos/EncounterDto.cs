using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos
{
    [JsonDerivedType(typeof(EncounterDto), typeDiscriminator: "base")]
    [JsonDerivedType(typeof(MiscEncounterDto), typeDiscriminator: "miscEncounter")]
    [JsonDerivedType(typeof(SocialEncounterDto), typeDiscriminator: "socialEncounter")]
    [JsonDerivedType(typeof(HiddenLocationEncounterDto), typeDiscriminator: "locationEncounter")]
    public class EncounterDto
    {
        public long Id {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EncounterLocationDto EncounterLocation { get; set; }
        public EncounterStatus EncounterStatus { get; set; }
        public int ExperiencePoints { get; set; }
        public EncounterType EncounterType { get; set; }
    }
    public enum EncounterStatus
    {
        Active = 0,
        Draft = 1,
        Archieved = 2,
        Pending = 3,
        Canceled = 4,
        Completed = 5
    }
    public enum EncounterType
    {
        Social = 0,
        Location = 1,
        Misc = 2
    }
}