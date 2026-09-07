using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.Payments
{
    public class Payment:Entity
    {
        private List<PaymentItem> _paymentItems = new();
        public long TouristId { get; private set; }
        public IReadOnlyList<PaymentItem> PaymentItems => new List<PaymentItem>(_paymentItems);
        public DateTime PurchaseDate { get; private set; }

        public Payment(long TouristId, DateTime PurchaseDate)
        {
            this.TouristId = TouristId;
            this.PurchaseDate = PurchaseDate;
        }

        public void AddToPayment(PaymentItem paymentItem)
        {
            _paymentItems.Add(paymentItem);
        }

        public PaymentItem GetById(long id)
        {
            var paymentItem = _paymentItems.FirstOrDefault(pi => pi.Id == id);
            if (paymentItem is null) throw new ArgumentException("Payment Item does not exits");
            return paymentItem;
        }
    }
}
