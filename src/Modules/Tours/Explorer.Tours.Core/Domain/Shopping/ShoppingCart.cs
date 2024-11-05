using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain.Shopping
{
    public class ShoppingCart : Entity
    {
        public List<OrderItem> Items { get; private set; }
        public long TouristId { get; private set; }
        public decimal TotalPrice { get; private set; }

        public ShoppingCart() { }

        public ShoppingCart(long userId)
        {
            TouristId = userId;
            Items = new List<OrderItem>();
            CalculateTotalPrice();
        }

        public void CalculateTotalPrice()
        {
            TotalPrice = 0;
            foreach (var item in Items)
            {
                TotalPrice += item.Price * item.Quantity;
            }
        }

        public void AddItemToCart(OrderItem orderItem)
        {
            if (Items.Contains(orderItem)) throw new Exception("Items list already contains that item");

            TotalPrice += orderItem.Price;
            Items.Add(orderItem);
        }
    }
}