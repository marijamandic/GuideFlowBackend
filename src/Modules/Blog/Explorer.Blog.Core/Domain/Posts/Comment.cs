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
    public class Comment : ValueObject<Comment>
    {
        public long UserId { get; private set; }
        public long PostId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string Content { get; set; } = string.Empty;
        public DateTime LastModified { get; private set; }

        public Comment() { }

        [JsonConstructor] //kako radi ovo?
        public Comment(long userId, long postId, DateTime createdAt, string content, DateTime lastModified)
        {
            UserId = userId;
            PostId = postId;
            CreatedAt = createdAt;
            Content = content;
            LastModified = lastModified;
            Validate();
        }

        internal static Result<Comment> Create(
            long userId,
            long postId,
            DateTime createdAt,
            string content,
            DateTime lastModified)
        {
            //da validacija vraca Result.Failure?
            
            var comment = new Comment(userId, postId, createdAt, content, lastModified);

            return comment; 
        }

        protected override bool EqualsCore(Comment other)
        {
            return UserId == other.UserId
                && PostId == other.PostId
                && CreatedAt == other.CreatedAt
                && Content == other.Content
                && LastModified == other.LastModified;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = UserId.GetHashCode();
                hashCode = (hashCode * 397) ^ PostId.GetHashCode();
                hashCode = (hashCode * 397) ^ CreatedAt.GetHashCode();
                hashCode = (hashCode * 397) ^ Content.GetHashCode();
                hashCode = (hashCode * 397) ^ LastModified.GetHashCode();
                return hashCode;
            }
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
