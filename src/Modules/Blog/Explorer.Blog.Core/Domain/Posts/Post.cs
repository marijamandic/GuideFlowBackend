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

        // Method to update Title with validation
        public void UpdateTitle(string newTitle)
        {
            if (string.IsNullOrWhiteSpace(newTitle))
                throw new ArgumentException("Title cannot be empty");

            Title = newTitle;
        }

        // Method to update Description
        public void UpdateDescription(string newDescription)
        {
            if (string.IsNullOrWhiteSpace(newDescription))
                throw new ArgumentException("Description cannot be empty");

            Description = newDescription;
        }

        // Method to update Status
        public void UpdateStatus(PostStatus newStatus)
        {
            Status = newStatus;
        }

        // Comment-related methods
        public Result AddComment(long id, long userId, long postId, DateTime createdAt, string content, DateTime lastModified)
        {
            var result = Comment.Create(id, userId, postId, createdAt, content, lastModified);
            if (result.IsFailed)
            {
                return Result.Fail("Error adding comment!");
            }
            _comments.Add(result.Value);
            return Result.Ok();
        }

        public Result UpdateComment(Comment updatedComment)
        {
            var comment = _comments.FirstOrDefault(c => c.Id == updatedComment.Id);

            if (comment == null)
            {
                return Result.Fail("Comment not found.");
            }

            comment.UpdateContent(updatedComment.Content);
            return Result.Ok();
        }


        public Result DeleteComment(long userId, long postId, DateTime createdAt)
        {
            var comment = _comments.FirstOrDefault(c =>
                c.UserId == userId &&
                c.PostId == postId &&
                c.CreatedAt == createdAt);

            if (comment == null)
            {
                return Result.Fail("Comment not found.");
            }

            _comments.Remove(comment);
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

        public Result DeleteComment(long commentId)
        {
            var comment = _comments.FirstOrDefault(c => c.Id == commentId);
            if (comment == null)
                return Result.Fail("Comment not found.");

            _comments.Remove(comment);
            return Result.Ok();
        }
    }

    public enum PostStatus
    {
        Draft,
        Published,
        Closed
    }
}
