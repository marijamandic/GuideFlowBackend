using Explorer.BuildingBlocks.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Explorer.Blog.Core.Domain.Posts
{
    public class Post : Entity
    {
        private readonly List<Comment> _comments = new();
        private readonly List<BlogRating> _ratings = new();

        public string Title { get; private set; }
        public long UserId { get; private set; }
        public string Description { get; private set; }
        public DateTime PublishDate { get; private set; }
        public string ImageUrl { get; private set; }
        public PostStatus Status { get; private set; }

        public Post() { }

        public Post(string title, long userId, string description, DateTime publishDate, string imageUrl, PostStatus status)
        {
            Title = title;
            UserId = userId;
            Description = description;
            PublishDate = publishDate;
            ImageUrl = imageUrl;
            Status = status;
            Validate();
        }

        public List<Comment> Comments => _comments.ToList();
        public List<BlogRating> Ratings => _ratings.ToList();

        public Result AddComment(long userId, long postId, DateTime createdAt, string content, DateTime lastModified)
        {
            var result = Comment.Create(userId, postId, createdAt, content, lastModified);
            if (result.IsFailed)
            {
                return Result.Fail("Greska pri dodavanju komentara!");
            }
            _comments.Add(result.Value);

            return Result.Ok();
        }

        public Result AddRating(long userId, long postId, DateTime ratingDate, long upVotesNumber, long downVotesNumber)
        {
            var result = BlogRating.Create(userId, postId, ratingDate, upVotesNumber, downVotesNumber);
            if (result.IsFailed)
            {
                return Result.Fail("Greska pri dodavanju ocene bloga!");
            }
            _ratings.Add(result.Value);

            return Result.Ok();
        }

        private void Validate()
        {
            if (UserId == 0) throw new ArgumentException("Invalid UserId");
            if (string.IsNullOrWhiteSpace(Title)) throw new ArgumentException("Invalid Title");
            if (PublishDate == DateTime.MinValue) throw new ArgumentException("Invalid PublishDate");
        }
    }

    public enum PostStatus
    {
        Draft,
        Published,
        Closed
    }
}
