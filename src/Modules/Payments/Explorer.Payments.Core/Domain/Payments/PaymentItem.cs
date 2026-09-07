using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.Payments
{
    public class PaymentItem : Entity
    {
        public long PaymentId { get; private set; }
        public long ProductId { get; private set;}
        public string ProductName { get; private set;}
        public ProductType Type { get; private set;}
        public int AdventureCoin { get; private set;}

        public PaymentItem(long PaymentId, long ProductId, ProductType Type, string ProductName, int AdventureCoin)
        {
            this.PaymentId = PaymentId;
            this.ProductId = ProductId;
            this.Type = Type;
            this.ProductName = ProductName;
            this.AdventureCoin = AdventureCoin;
            Validate();
        }

        public void Validate()
        {
            if (!Enum.IsDefined(typeof(ProductType), Type)) throw new ArgumentException("Invalid Product Type");
            if (string.IsNullOrWhiteSpace(ProductName)) throw new ArgumentException("Invalid Product Name");
            if (AdventureCoin < 0) throw new ArgumentException("Invalid Adventure Coin");
        }
    }
}
