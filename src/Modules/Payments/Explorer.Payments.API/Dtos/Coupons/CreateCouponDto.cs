using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos.Coupons
{
    public class CreateCouponDto
    {
        public long AuthorId { get; set; } 
        public long? TourId { get; set; } 
        public double Discount { get; set; }
        public DateTime? ExpiryDate { get; set; } 
        public bool ValidForAllTours { get; set; }
    }
}
