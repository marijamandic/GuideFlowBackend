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

        public long UserId { get; private set; }
        public long EncounterId { get; private set; }
        public ExecutionStatus ExecutionStatus { get; private set; }
        public bool IsComplete { get; private set; }
        public Encounter Encounter { get; private set; }
        public EncounterExecution(long encounterId, long userId) 
        {
            EncounterId = encounterId;
            UserId = userId;
            ExecutionStatus = ExecutionStatus.Active;
            IsComplete = false;
        }

        public void CompleteSocialEncounter()
        {
            //TODO: za svakog turistu se resi tura???
            if (ExecutionStatus.Equals(ExecutionStatus.Active)/* && TouristsIncluded.Count() >= SocialEncounter.TouristNumber*/)
            {
                ExecutionStatus = ExecutionStatus.Completed;
                IsComplete = true;
            }
        }

        public void Complete()
        {
            if (ExecutionStatus.Equals(ExecutionStatus.Active))
            {
                IsComplete = true;
            }
        }

        //da li turista moze da aktivira izazov ( za sva tri)
        public bool IsTouristNear(double latitude, double longitude)
        {
            double tolerance;// Tolerancija za blizinu

            if (Encounter.EncounterType == EncounterType.Location && Encounter is HiddenLocationEncounter hiddenLocationEncounter)
                tolerance = hiddenLocationEncounter.ActivationRange;
            else
                tolerance = 0.00245;

            bool isNearLatitude = Math.Abs(Encounter.EncounterLocation.Latitude - latitude) <= tolerance;
            bool isNearLongitude = Math.Abs(Encounter.EncounterLocation.Longitude - longitude) <= tolerance;

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
