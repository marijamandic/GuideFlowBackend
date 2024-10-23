using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public enum Status { Added = 0, Removed = 1}
    public class EquipmentManagement : Entity
    {
        public int TouristId { get; private set; }
        public int EquipmentId { get; private set; }
        public Status Status { get; private set; }
        //public List<Equipment> EquipmentList { get; private set; } 

        public EquipmentManagement() { }

        public EquipmentManagement(int touristId, int equipmentId, Status status)
        {
            if (touristId <= 0)
                throw new ArgumentException("Tourist ID must be a positive number.", nameof(touristId));

            if (equipmentId <= 0)
                throw new ArgumentException("Equipment ID must be a positive number.", nameof(equipmentId));

            TouristId = touristId;
            EquipmentId = equipmentId;
            Status = status;
            //EquipmentList = new List<Equipment>();
        }
    }
}
