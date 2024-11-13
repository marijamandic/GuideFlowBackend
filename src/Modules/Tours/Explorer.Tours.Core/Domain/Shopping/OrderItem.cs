using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.Shopping
{
    public class OrderItem : Entity
    {
        public long TourID { get; set; }
        public string TourName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public OrderItem(long tourID, string tourName, decimal price, int quantity = 1)
        {
            TourID = tourID;
            TourName = tourName;
            Price = price;
            Quantity = quantity;

            Validate();
        }

        private void Validate()
        {
            if (Price <= 0)
                throw new ArgumentException("Price must be greater than zero.");
            if (Quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");
        }

        public void IncrementQuantity(int addQuantit)
        {
            Quantity += addQuantit;
        }
    }

}