using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos.Payments
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public DateTime PurchaseDate { get; set; }

        public List<PaymentItemDto> PaymentItems = new();
    }
}
