using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.WeatherForecast
{
    public class WeatherResponse
    {
        public Main Main { get; set; }
        public Weather[] Weather { get; set; }
        public double Visibility { get; set; }
        public Wind Wind { get; set; }
        public string Name { get; set; }
    }
}
