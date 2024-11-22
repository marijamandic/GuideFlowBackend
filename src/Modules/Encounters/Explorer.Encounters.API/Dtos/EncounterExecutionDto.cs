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

        public int UserIs { get; set; }
        public int encounterId { get; private set; }

        public bool isComplete { get; private set; }

        public List<User> touristsIncluded { get; private set; } 
    }
}
