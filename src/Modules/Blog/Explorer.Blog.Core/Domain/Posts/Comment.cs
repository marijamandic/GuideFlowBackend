using Explorer.BuildingBlocks.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Explorer.Blog.Core.Domain.Posts
{
    public class Comment : Entity
    {
        public long UserId { get; private set; }
        public long PostId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string Content { get; set; } = string.Empty;
        public DateTime LastModified { get; private set; }

        public Comment() { }

        [JsonConstructor]
        public Comment(long userId, long postId, DateTime createdAt, string content, DateTime lastModified)
        {
            UserId = userId;
            PostId = postId;
            CreatedAt = createdAt;
            Content = content;
            LastModified = lastModified;
            Validate();
        }

        public void UpdateContent(string newContent)
        {
            if (string.IsNullOrWhiteSpace(newContent))
                throw new ArgumentException("Content cannot be empty");

            Content = newContent;
            UpdateLastModified();
        }

        private void UpdateLastModified()
        {
            LastModified = DateTime.UtcNow;
        }

        internal static Result<Comment> Create(
        long userId,
        long postId,
        DateTime createdAt,
        string content,
        DateTime lastModified)
        {
            var comment = new Comment(userId, postId, createdAt, content, lastModified);
            return comment;
        }

        private void Validate()
        {
            if (UserId == 0) throw new ArgumentException("Invalid UserId");
            if (PostId == 0) throw new ArgumentException("Invalid PostId");
            if (string.IsNullOrWhiteSpace(Content)) throw new ArgumentException("Content cannot be empty");
            if (CreatedAt >= DateTime.UtcNow) throw new ArgumentException("CreatedAt cannot be in the future");
            if (LastModified < CreatedAt) throw new ArgumentException("LastModified cannot be before CreatedAt");
        }
    }
}
