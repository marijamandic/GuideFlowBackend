using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.Shopping
{
    public class PurchaseToken: Entity
    {
        public int UserId { get; set; }
        public int TourId { get; set; }

        public PurchaseToken() { }

        public PurchaseToken(int userId, int tourId)
        {
            UserId = userId;
            TourId = tourId;
        }
    }
}
