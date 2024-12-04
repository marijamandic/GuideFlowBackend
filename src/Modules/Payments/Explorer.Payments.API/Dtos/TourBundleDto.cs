using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
    public class TourBundleDto
    {
        public long Id { get; set; }    
        public string Name { get; set; }

        public double Price { get; set; }

        public BundleStatus Status { get; set; }

        public int AuthorId { get; set; }

        public List<int> TourIds { get; set; } = new List<int>();
        
    }
    public enum BundleStatus
    {
        Draft,
        Published,
        Archieved
    }
}
