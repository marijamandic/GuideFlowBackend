using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.WeatherForecast
{
    public class ForecastWeatherResponse
    {
        public City City { get; set; }
        public List<ForecastItem> List { get; set; }
    }
}
