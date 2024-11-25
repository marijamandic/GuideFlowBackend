using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Dtos.Execution;
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

        public ExecutionStatus ExecutionStatus { get; private set; }

        public bool isComplete { get; private set; }

        public List<User> touristsIncluded { get; private set; } // turisti koji ucestvuju u izazovu

        public SocialEncounter socialEncounter { get; private set; }
        public MiscEncounter miscEncounter { get; private set; }
        public HiddenLocationEncounter hiddenLocationEncounter { get; private set; }

        public Encounter encounter { get; private set; }
        private readonly IEncountersRepository encountersRepository;
        public EncounterExecution() 
        {
            encounter = encountersRepository.Get(encounterId);
            socialEncounter = (SocialEncounter?)encountersRepository.Get(encounterId);
            miscEncounter = (MiscEncounter?)encountersRepository.Get(encounterId);
            hiddenLocationEncounter = (HiddenLocationEncounter?)encountersRepository.Get(encounterId);

        }

    


        public void CompleteSocialEncounter()
        {
            //TODO: za svakog turistu se resi tura???
            if(ExecutionStatus.Equals(ExecutionStatus.Active) && touristsIncluded.Count() >= socialEncounter.TouristNumber)
            {
                ExecutionStatus = ExecutionStatus.Completed;
                isComplete = true;
            }
        }

        public void Complete()
        {
            if (ExecutionStatus.Equals(ExecutionStatus.Active))
            {
                isComplete = true;
            }
        }

        //da li turista moze da aktivira izazov ( za sva tri)
        public bool IsTouristNear(double latitude, double longitude)
        {
            double tolerance;// Tolerancija za blizinu

            if (encounter.EncounterType == EncounterType.Location)
                tolerance = hiddenLocationEncounter.ActivationRange;
            else
                tolerance = 0.00245;

            bool isNearLatitude = Math.Abs(encounter.EncounterLocation.Latitude - latitude) <= tolerance;
            bool isNearLongitude = Math.Abs(encounter.EncounterLocation.Longitude - longitude) <= tolerance;

            return isNearLatitude && isNearLongitude;
        }




        //TODO: azuriranje liste turista koji su u opsegu
        //funkcija Complete(brTurista)
        //funkcija Pridruzi dodaje tog usera u touristsIncluded i poredi sa touristNumber u Encounteru
        //create: da li postoji vec execution za encId i da li je resen

    }

    public enum ExecutionStatus
    {
        Active,
        Completed,
        Abandoned
    }

}
