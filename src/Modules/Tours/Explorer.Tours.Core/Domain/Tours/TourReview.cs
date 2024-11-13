using Explorer.BuildingBlocks.Core.Domain;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.Tours
{
    public class TourReview : Entity
    {
        public int Rating { get; private set; }
        public string? Comment { get; private set; }
        public DateTime TourDate { get; private set; }
        public DateTime CreationDate { get; private set; }
        public int PercentageCompleted { get; private set; }
        public int TouristId { get; private set; }
        public long TourId { get; private set; }
        public TourReview(int rating, string comment, DateTime tourDate, DateTime creationDate, int percentageCompleted, int touristId, long tourId)
        {
            Rating = rating;
            Comment = comment;
            TourDate = tourDate;
            CreationDate = creationDate;
            Validate();
            PercentageCompleted = percentageCompleted;
            TouristId = touristId;
            TourId = tourId;
        }

        private void Validate()
        {
            if (Rating < 1 || Rating > 5)
                throw new ArgumentException("Rating must be between 1 and 5.");

            if (string.IsNullOrWhiteSpace(Comment))
                throw new ArgumentException("Comment cannot be empty. ");

            if (TourDate > DateTime.Now)
                throw new ArgumentException("Tour date cannot be in the future.");

            if (CreationDate < TourDate)
                throw new ArgumentException("Creation date cannot be before the tour date.");

            if (PercentageCompleted < 0 || PercentageCompleted > 100)
                throw new ArgumentException("Percentage completed must be between 0 and 100.");
        }
    }
}
