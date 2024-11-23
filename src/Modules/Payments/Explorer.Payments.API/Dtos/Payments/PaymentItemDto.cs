using Explorer.Payments.API.Dtos.ShoppingCarts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos.Payments
{
    public class PaymentItemDto
    {
        public int Id { get; set; }
        public int PaymentId { get; set; }
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public ProductType Type { get; set; }
        public int AdventureCoin { get; set; }
    }
}
