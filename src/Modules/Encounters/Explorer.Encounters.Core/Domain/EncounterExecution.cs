using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
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

        public double UserLongitude { get; set; }
        public double UserLatitude { get; set; }

        public int Participants { get; private set; }
        public EncounterExecution(long encounterId, long userId) 
        {
            EncounterId = encounterId;
            UserId = userId;
            ExecutionStatus = ExecutionStatus.Active;
            IsComplete = false;
        }

        public void CompleteSocialEncounter()
        {
            if (ExecutionStatus.Equals(ExecutionStatus.Active) && Encounter is SocialEncounter socialEncounter)
            {
                if(socialEncounter.TouristNumber <= Participants)
                {
                    IsComplete = true;
                }
                else
                {
                    IsComplete= false;
                }
            }
        }

        public void CountParticipants(int participants)
        {
            Participants = participants;
        }

        public void Complete(EncounterExecution encounterExecution)
        {
            if (ExecutionStatus.Equals(ExecutionStatus.Active))
            {
                IsComplete = true;
                ExecutionStatus = ExecutionStatus.Completed;
                UserLongitude = encounterExecution.UserLongitude;
                UserLatitude = encounterExecution.UserLatitude;
            }
        }

        //da li turista moze da aktivira izazov ( za sva tri)
        public bool IsTouristNear(Encounter encounter)
        {
            // double tolerance;// Tolerancija za blizinu
            double latTolerance;
            double lonTolerance;
            double longitude = UserLongitude;
            double latitude = UserLatitude;

            if (encounter.EncounterType == EncounterType.Location && encounter is HiddenLocationEncounter hiddenLocationEncounter)
            {
                latTolerance = hiddenLocationEncounter.ActivationRange / 111320.0;
                lonTolerance = hiddenLocationEncounter.ActivationRange / (111320.0 * Math.Cos(latitude));
            }
            else if (encounter.EncounterType == EncounterType.Social && encounter is SocialEncounter socialEncounter)
            {
                latTolerance = socialEncounter.EncounterRange / 111320.0;
                lonTolerance = socialEncounter.EncounterRange / (111320.0 * Math.Cos(latitude));
            }
            else
            {
                latTolerance = 20.0 / 111320.0;   // 20 metara
                lonTolerance = 20.0 / (111320.0 * Math.Cos(latitude));
            }

            bool isNearLatitude = Math.Abs((double)(encounter.EncounterLocation.Latitude - latitude)) <= latTolerance;
            bool isNearLongitude = Math.Abs((double)(encounter.EncounterLocation.Longitude - longitude)) <= lonTolerance;

            return isNearLatitude && isNearLongitude;
        }

        public bool IsHiddenLocationFound(double latitude, double longitude, Encounter encounter)
        {
            double latTolerance = 20.0 / 111320.0;   // 10 metara
            double lonTolerance = 20.0 / (111320.0 * Math.Cos(latitude));

            if (encounter is HiddenLocationEncounter hiddenLocationEncounter)
            {
                bool isNearLatitude = Math.Abs(hiddenLocationEncounter.ImageLatitude - latitude) <= latTolerance;
                bool isNearLongitude = Math.Abs(hiddenLocationEncounter.ImageLongitude - longitude) <= lonTolerance;

                return isNearLatitude && isNearLongitude;
            }

            return false;
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
