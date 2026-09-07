using Explorer.Tours.Core.Domain.WeatherForecast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.WeatherForecast
{
    public class ForecastItem
    {
        public long dt { get; set; }
        public DateTime Dt => DateTimeOffset.FromUnixTimeSeconds(dt).DateTime;
        public Main Main { get; set; }
        public Weather[] Weather { get; set; }
        public Wind Wind { get; set; }
    }
}
