using Explorer.BuildingBlocks.Core.Domain;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Explorer.Stakeholders.Core.Domain
{
    public class AppRating : Entity
    {
        public long RatingId { get; init; }
        public long UserId { get; init; }

        public int Rating {  get; init; } // 1 d0 5
        public string Comment {  get; init; }
        public DateTime RatingTime { get; init; }

        public AppRating(long ratingId, long userId, int rating, string comment, DateTime ratingTime)
        {
            RatingId = ratingId;
            UserId = userId;
            Rating = rating;
            Comment = comment;
            RatingTime = ratingTime;
            Validate();
        }

        private void Validate()
        {
            if (RatingId == 0) throw new ArgumentException("Invalid RatingId");
            if (UserId == 0) throw new ArgumentException("Invalid UserId");
            if (Rating < 1 || Rating > 5) throw new ArgumentException("Rating must be between 1 and 5");
            if (string.IsNullOrWhiteSpace(Comment)) throw new ArgumentException("Invalid Comment");

            if (RatingTime == default(DateTime)) throw new ArgumentException("Invalid RatingTime");
            if (RatingTime > DateTime.UtcNow) throw new ArgumentException("RatingTime cannot be in the future");
        }

    }
}
