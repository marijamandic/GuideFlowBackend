using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
    public class TourBundleDto
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public Status Status { get; set; }

        public List<long> TourIds { get; set; } = new List<long>();
        
    }
    public enum Status
    {
        Draft,
        Published,
        Archieved
    }
}
