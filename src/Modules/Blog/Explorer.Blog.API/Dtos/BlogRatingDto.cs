using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Dtos
{
    public class BlogRatingDto
    {
        public long UserId { get; set; }
        public long PostId { get; set; }
        public DateTime RatingDate { get; set; }
        public long UpVotesNumber { get; set; }
        public long DownVotesNumber { get; set; }
    }
}
