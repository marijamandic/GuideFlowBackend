using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain
{
    public class EncounterExecution: Entity
    {
        public int userId { get; private set; }

        //public EncounterType encounterType;

        public int encounterId { get; private set; }

        public bool isComplete { get; private set; }

        public List<User> touristsIncluded { get; private set; } // turisti koji ucestvuju u izazovu

        public EncounterExecution() { }




        //TODO: azuriranje liste turista koji su u opsegu
        //funkcija Complete(brTurista)
        //funkcija Pridruzi dodaje tog usera u touristsIncluded i poredi sa touristNumber u Encounteru
        //create: da li postoji vec execution za encId i da li je resen

    }

  

}
