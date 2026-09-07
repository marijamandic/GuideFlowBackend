using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.Shopping
{
    public class OrderItemDto
    {
        public long TourID { get; set; }
        public string TourName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public OrderItemDto () { }

        public OrderItemDto(long tourID, string tourName, decimal price, int quantity)
        {
            TourID = tourID;
            TourName = tourName;
            Price = price;
            Quantity = quantity;
        }
    }
}
