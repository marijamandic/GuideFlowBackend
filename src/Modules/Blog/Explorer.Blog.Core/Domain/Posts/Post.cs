using Explorer.Blog.API.Dtos;
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

        public string Title { get; set; }
        public long UserId { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public string ImageUrl { get; set; }
        public PostStatus Status { get; set; }

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

        public Result AddRating(BlogRatingDto blogRatingDto)
        {
            var result = BlogRating.Create(blogRatingDto.UserId, blogRatingDto.PostId, blogRatingDto.RatingDate, blogRatingDto.UpVotesNumber, blogRatingDto.DownVotesNumber);
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
