﻿using Explorer.Payments.API.Dtos.ShoppingCarts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
    public class TourPurchaseTokenDto
    {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public int TourId { get; set; }
    }
}
