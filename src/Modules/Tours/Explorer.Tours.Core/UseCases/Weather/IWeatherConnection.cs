using Explorer.Tours.Core.Weather.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Weather
{
    public interface IWeatherConnection
    {
        Task<WeatherResponse> GetWeatherAsync(double latitude, double longitude);
    }
}
