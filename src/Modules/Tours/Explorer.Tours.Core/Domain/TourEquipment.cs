using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class TourEquipment : Entity
    {
        public int TourId { get; init; }
        public int EquipmentId { get; init; }
        public int Quantity { get; init; }

        public TourEquipment( int tourId, int eqId)
        {
            TourId = tourId;
            EquipmentId = eqId;
            Quantity = 0;
        }
    }
}
