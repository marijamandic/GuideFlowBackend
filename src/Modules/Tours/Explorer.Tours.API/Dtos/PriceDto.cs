using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class PriceDto
    {
        public double Cost { get; set; }
        public Currency Currency { get; set; }
    }

    public enum Currency
    {
        RSD,
        EUR,
        USD
    }
}
