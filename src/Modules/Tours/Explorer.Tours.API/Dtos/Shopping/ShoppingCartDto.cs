﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.Shopping
{
    public class ShoppingCartDto
    {
        public long? Id { get; set; }
        public List<OrderItemDto> Items { get; set; }
        public long UserId { get; set; }
        public decimal TotalPrice { get; set; }

        public ShoppingCartDto() { }

        public ShoppingCartDto(long id, List<OrderItemDto> items, long userId, long totalPrice)
        {
            Id = id;
            Items = items;
            UserId = userId;
            TotalPrice = totalPrice;
        }
    }
}
