using Explorer.BuildingBlocks.Core.Domain;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Explorer.Blog.Core.Domain
{

    public class Post : Entity
    {
        public string Title { get; private set; }
        public long UserId { get; private set; }
        public string Description { get; private set; }
        public DateTime PublishDate { get; private set; }
        public string ImageUrl { get; private set; }
        public PostStatus Status { get; private set; }

        public Post() { }

        public Post(string title , long userId , string description , DateTime publishDate , string imageUrl , PostStatus status) { 
            Title = title;
            UserId = userId;
            Description = description;
            PublishDate = publishDate;
            ImageUrl = imageUrl;
            Status = status;
            Validate();
        }

        private void Validate() {
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
