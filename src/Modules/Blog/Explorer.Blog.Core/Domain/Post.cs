using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain
{

    public class Post : Entity
    {
        public string Title { get; set; }
        public long UserId { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public string ImageUrl { get; set; }
        public Status Status { get; set; }

    }
}
public enum Status
{
    Draft,
    Published,
    Closed
}
