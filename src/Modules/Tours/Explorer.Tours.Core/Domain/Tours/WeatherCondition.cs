using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.Tours
{
    public class WeatherCondition : ValueObject<WeatherCondition>
    {
        public int MinTemperature { get; private set; }
        public int MaxTemperature { get; private set; }
        public List<WeatherConditionType> SuitableConditions { get; private set; }

        [JsonConstructor]
        public WeatherCondition(int minTemperature,int maxTemperature,List<WeatherConditionType> suitableConditions)
        {
            MinTemperature = minTemperature;
            MaxTemperature = maxTemperature;
            SuitableConditions = suitableConditions;
            Validate();
        }

        private void Validate()
        {
            if (MinTemperature > MaxTemperature)
                throw new ArgumentException("MinTemperature cannot be greater than MaxTemperature.");
            if (SuitableConditions == null || !SuitableConditions.Any())
                throw new ArgumentException("SuitableConditions cannot be null or empty.");
        }


        protected override bool EqualsCore(WeatherCondition other)
        {
            if (other == null)
                return false;

            return MinTemperature == other.MinTemperature &&
                   MaxTemperature == other.MaxTemperature &&
                   SuitableConditions.SequenceEqual(other.SuitableConditions);
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = MinTemperature.GetHashCode();
                hashCode = (hashCode * 397) ^ MaxTemperature.GetHashCode();
                if (SuitableConditions != null && SuitableConditions.Any())
                {
                    foreach (var condition in SuitableConditions)
                    {
                        hashCode = (hashCode * 397) ^ condition.GetHashCode();
                    }
                }
                return hashCode;
            }
        }

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
