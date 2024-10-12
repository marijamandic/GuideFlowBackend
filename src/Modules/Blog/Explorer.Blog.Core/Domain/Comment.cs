using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain
{
    public class Comment : Entity
    {
        public long UserId { get; set; }
        public long PostId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Content { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
