using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain
{
    public class HiddenLocationEncounter : Encounter
    {
        public string ImageUrl { get; private set; }
        public double ActivationRange { get; private set; }
        public long CheckpointId { get; private set; }
        public double  ImageLatitude { get; private set; }
        public double ImageLongitude { get; private set; }

        public HiddenLocationEncounter() { }
        public HiddenLocationEncounter(string name,string description,EncounterLocation location,EncounterStatus status,int experiencePoints,EncounterType encounterType,
                                 string imageUrl,double activationRange,long checkpointId,
                                 double imageLatitude, double imageLongitude): base(name, description, location, status, experiencePoints, encounterType)
        {
            ImageUrl = imageUrl;
            ActivationRange = activationRange;
            CheckpointId = checkpointId;
            ImageLatitude = imageLatitude;
            ImageLongitude = imageLongitude;
            Validate();
        }
        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(ImageUrl))
                throw new ArgumentException("ImageUrl cannot be null or empty.");

            if (ActivationRange <= 0)
                throw new ArgumentException("ActivationRange must be greater than zero.");

            if (CheckpointId <= 0)
                throw new ArgumentException("CheckpointId must be a positive number.");
        }
    }
}
