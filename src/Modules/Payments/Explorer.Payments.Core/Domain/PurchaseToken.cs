using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain
{
    public class PurchaseToken : Entity
    {
        public long TouristId { get; private set; }
        public ProductType Type { get; private set; }
        public long ProductId { get; private set; }
        public DateTime PurchaseDate { get; private set; }
        public int AdventureCoin { get; private set; }

        public PurchaseToken(long Id, long TouristId, ProductType Type, long ProductId, DateTime PurchaseDate, int AdventureCoin)
        {
            this.Id = Id;
            this.TouristId = TouristId;
            this.Type = Type;
            this.ProductId = ProductId;
            this.PurchaseDate = PurchaseDate;
            this.AdventureCoin = AdventureCoin;
            Validate();
        }

        private void Validate()
        {
            if (TouristId == 0) throw new ArgumentException("Invalid tourist id!");
            if (!Enum.IsDefined(typeof(ProductType), Type)) throw new ArgumentException("Invalid transport type value.");
            if (ProductId == 0) throw new ArgumentException("Invalid tour id!");
            if (AdventureCoin < 0) throw new ArgumentException("Price can't be less than 0!");
        }
    }
}
