using Explorer.BuildingBlocks.Core.Domain;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Explorer.Payments.Core.Domain.PurchaseTokens
{
    public class TourPurchaseToken:Entity
    {
        public long TouristId { get; private set; }
        public long TourId {  get; private set; }
        public DateTime PurchaseDate { get; private set; }
        public int AdventureCoin { get; private set; }

        public TourPurchaseToken(long Id,long TouristId,long TourId,DateTime PurchaseDate,int AdventureCoin)
        {
            this.Id = Id;
            this.TouristId = TouristId;
            this.TourId = TourId;
            this.PurchaseDate = PurchaseDate;
            this.AdventureCoin = AdventureCoin;
            Validate();
        }

        private void Validate()
        {
            if (TouristId == 0) throw new ArgumentException("Invalid tourist id!");
            if (TourId == 0) throw new ArgumentException("Invalid tour id!");
            if (AdventureCoin < 0) throw new ArgumentException("Price can't be less than 0!");
        }
    }
}
