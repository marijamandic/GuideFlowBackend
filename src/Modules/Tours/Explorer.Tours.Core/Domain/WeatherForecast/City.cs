using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.WeatherForecast
{
    public class City
    {
        public string Name { get; set; }
        public Coord Coord { get; set; }
        public string Country { get; set; }
        public int Timezone { get; set; }
    }
    public class Coord
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}
