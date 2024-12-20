using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourDto
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string? AuthorName {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Level Level { get; set; }
        public TourStatus Status { get; set; }
        public DateTime? StatusChangeDate {  get; set; }
        public double LengthInKm { get; set; }
        public WeatherConditionDto WeatherRequirements { get; set; }
        public bool IsPremium { get; set; } = false;
        public int Price { get; set; }
        public double AverageGrade { get; set; }
        //Weather
        public string? WeatherIcon { get; set; }
        public double? Temperature {  get; set; }
        public string? WeatherDescription { get; set; }
        public WeatherRecommend? WeatherRecommend { get; set; }

        public List<string> Taggs { get; set; }
        public List<CheckpointDto> Checkpoints { get; set; }
        public List<TransportDurationDto> TransportDurations { get; set; }
        public List<TourReviewDto> Reviews { get; set; }
    }

    public enum TourStatus
    {
        Draft,
        Published,
        Archived,
        Deleted
    }

    public enum Level
    {
        Easy,
        Advanced,
        Expert
    }
    public enum WeatherRecommend
    {
        HighyRecommend,
        Recommend,
        Neutral,
        DontRecommend,
        HighlyDontRecommend
    }
}
