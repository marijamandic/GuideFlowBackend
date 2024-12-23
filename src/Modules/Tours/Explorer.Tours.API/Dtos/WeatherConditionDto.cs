using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class WeatherConditionDto
    {
        public int MinTemperature { get; set; }
        public int MaxTemperature { get; set; }
        public List<WeatherConditionType> SuitableConditions { get; set; }
    }
    public enum WeatherConditionType
    {
        CLEAR,
        CLOUDS,
        RAIN,
        SNOW,
        MIST
    }
}
