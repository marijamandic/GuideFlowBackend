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
    [JsonDerivedType(typeof(LocationEncounterDto), typeDiscriminator: "locationEncounter")]
    public class EncounterDto
    {
        public long Id {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EncounterLocationDto EncounterLocation { get; set; }
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