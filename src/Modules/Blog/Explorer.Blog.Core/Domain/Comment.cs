using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Explorer.Blog.Core.Domain
{
    public class Comment : Entity
    {
        public long UserId { get; private set; }
        public long PostId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string Content { get; private set; }
        public DateTime? LastModified { get; private set; }

        public Comment(long userId, long postId, DateTime createdAt, string content,DateTime lastModified)
        {
            UserId = userId;
            PostId = postId;
            CreatedAt = createdAt;
            Content = content;
            LastModified = lastModified;
            Validate();
        }

        private void Validate()
        {
            if (UserId <= 0) throw new ArgumentException("Invalid UserId");
            if (PostId <= 0) throw new ArgumentException("Invalid PostId");
            if (string.IsNullOrWhiteSpace(Content)) throw new ArgumentException("Content cannot be empty");
            if (CreatedAt > DateTime.Now) throw new ArgumentException("CreatedAt cannot be in the future");
            if (LastModified < CreatedAt) throw new ArgumentException("LastModified cannot be before CreatedAt");
        }
    }
}
