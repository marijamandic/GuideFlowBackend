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
        public PriceDto Price { get; set; }
        public double AverageGrade { get; set; }
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
}
