using Explorer.Tours.Core.UseCases.Weather;
using Explorer.Tours.Core.Domain.WeatherForecast;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Weather
{
    public class WeatherConnection : IWeatherConnection
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        public WeatherConnection(HttpClient httpClient, IConfiguration configuration) { 
            _httpClient = httpClient;
            _apiKey = configuration["OpenWeatherMap:ApiKey"];

        }
        public async Task<WeatherResponse> GetWeatherAsync(double latitude, double longitude)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={_apiKey}&units=metric";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to fetch weather data.");

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<WeatherResponse>(json);
        }

        public async Task<ForecastWeatherResponse> GetFiveDayForecast(double latitude, double longitude)
        {
            var url = $"https://api.openweathermap.org/data/2.5/forecast?lat={latitude}&lon={longitude}&appid={_apiKey}&units=metric";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to fetch weather data.");

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ForecastWeatherResponse>(json);
        }

    }
}
