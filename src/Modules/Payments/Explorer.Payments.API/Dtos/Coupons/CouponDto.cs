using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos.Coupons
{
    public class CouponDto
    {
        public long Id { get; set; }
        public long AuthorId { get; set; }
        public long? TourId { get; set; } 
        public string Code { get; set; } 
        public double Discount { get; set; }
        public DateTime? ExpiryDate { get; set; } 
        public bool ValidForAllTours { get; set; }
        public bool Redeemed { get; set; }
    }
}
