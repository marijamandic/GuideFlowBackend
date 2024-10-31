using Explorer.BuildingBlocks.Core.Domain;
using FluentResults;
using System;
using System.Text.Json.Serialization;

namespace Explorer.Blog.Core.Domain.Posts
{
    public class BlogRating : ValueObject<BlogRating>
    {
        public long UserId { get; private set; }
        public long PostId { get; private set; }
        public DateTime RatingDate { get; private set; }
        public long UpVotesNumber { get; private set; }
        public long DownVotesNumber { get; private set; }

        public BlogRating() { }  

        [JsonConstructor]
        public BlogRating(long userId, long postId, DateTime ratingDate, long upVotesNumber, long downVotesNumber)
        {
            UserId = userId;
            PostId = postId;
            RatingDate = ratingDate;
            UpVotesNumber = upVotesNumber;
            DownVotesNumber = downVotesNumber;
            Validate();
        }

        public static Result<BlogRating> Create(
            long userId,
            long postId,
            DateTime ratingDate,
            long upVotesNumber,
            long downVotesNumber)
        {
            var rating = new BlogRating(userId, postId, ratingDate, upVotesNumber, downVotesNumber);
            return Result.Ok(rating);
        }

        protected override bool EqualsCore(BlogRating other)
        {
            return UserId == other.UserId
                && PostId == other.PostId
                && RatingDate == other.RatingDate
                && UpVotesNumber == other.UpVotesNumber
                && DownVotesNumber == other.DownVotesNumber;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = UserId.GetHashCode();
                hashCode = (hashCode * 397) ^ PostId.GetHashCode();
                hashCode = (hashCode * 397) ^ RatingDate.GetHashCode();
                hashCode = (hashCode * 397) ^ UpVotesNumber.GetHashCode();
                hashCode = (hashCode * 397) ^ DownVotesNumber.GetHashCode();
                return hashCode;
            }
        }

        private void Validate()
        {
            if (UserId == 0) throw new ArgumentException("Invalid UserId");
            if (PostId == 0) throw new ArgumentException("Invalid PostId");
            if (RatingDate > DateTime.UtcNow) throw new ArgumentException("RatingDate cannot be in the future");
            if (UpVotesNumber < 0) throw new ArgumentException("UpVotesNumber cannot be negative");
            if (DownVotesNumber < 0) throw new ArgumentException("DownVotesNumber cannot be negative");
        }
    }
}
