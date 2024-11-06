using Explorer.BuildingBlocks.Core.Domain;
using FluentResults;
using System;
using System.Text.Json.Serialization;

namespace Explorer.Blog.Core.Domain.Posts
{
    public enum RatingStatus
    {
        Plus,
        Minus
    }
    public class BlogRating : Entity
    {
        public long UserId { get; private set; }
        public long PostId { get; private set; }
        public DateTime RatingDate { get; private set; }
        
        public RatingStatus RatingStatus { get; private set; }

        public BlogRating() { }  

        [JsonConstructor]
        public BlogRating(long userId, long postId, DateTime ratingDate, RatingStatus ratingStatus)
        {
            UserId = userId;
            PostId = postId;
            RatingDate = ratingDate.ToUniversalTime(); // Ensure UTC
            RatingStatus = ratingStatus;
            Validate();
        }

        public static Result<BlogRating> Create(
            long userId,
            long postId,
            DateTime ratingDate,
            RatingStatus ratingStatus
            )
        {
            var rating = new BlogRating(userId, postId, ratingDate, ratingStatus);
            return Result.Ok(rating);
        }

        
        private void Validate()
        {
            if (UserId == 0) throw new ArgumentException("Invalid UserId");
            if (PostId == 0) throw new ArgumentException("Invalid PostId");
            if (RatingDate > DateTime.Now) throw new ArgumentException("RatingDate cannot be in the future");
            /*if (UpVotesNumber < 0) throw new ArgumentException("UpVotesNumber cannot be negative");
            if (DownVotesNumber < 0) throw new ArgumentException("DownVotesNumber cannot be negative");*/
        }
    }
}
