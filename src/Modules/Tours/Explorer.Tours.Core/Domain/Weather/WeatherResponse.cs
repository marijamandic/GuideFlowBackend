using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Weather.Models
{
    public class WeatherResponse
    {
        public Main Main { get; set; }
        public Weather[] Weather { get; set; }
        public string Name { get; set; }
    }
}
