using Explorer.Stakeholders.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos
{
    public class EncounterExecutionDto
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public int EncounterId { get; set; }
        public bool IsComplete { get; set; }
        public List<User> touristsIncluded { get; set; }
        public EncounterType EncounterType { get; set; }

    }
}
