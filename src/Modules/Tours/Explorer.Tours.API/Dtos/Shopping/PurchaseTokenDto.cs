using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.Shopping
{
    public class PurchaseTokenDto
    {
        public int Id { get; set; }
        public int UserId { get; set; } 
        public int TourId { get; set; }
    }
}
