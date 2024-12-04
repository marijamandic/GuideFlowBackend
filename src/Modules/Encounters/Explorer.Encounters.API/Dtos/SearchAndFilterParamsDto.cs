using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos
{
    public class SearchAndFilterParamsDto
    {
        public string? Name { get; set; }
        public int? Type { get; set; }
        public double? UserLatitude { get; set; }
        public double? UserLongitude { get; set; }
    }
}
