using Explorer.Payments.API.Dtos.ShoppingCarts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
    public class PurchaseTokenDto
    {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public ProductType Type { get; set; }
        public int ProductId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int AdventureCoin { get; set; }
    }
}
