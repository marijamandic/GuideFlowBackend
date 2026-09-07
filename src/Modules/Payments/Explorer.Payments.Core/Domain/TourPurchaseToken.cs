using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain
{
    public class TourPurchaseToken : Entity
    {
        public long TouristId { get; private set; }
        public long TourId { get; private set; }

        public TourPurchaseToken(long TouristId, long TourId)
        {
            this.TouristId = TouristId;
            this.TourId = TourId;
        }
    }
}
