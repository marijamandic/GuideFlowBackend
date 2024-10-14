using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class RatingAppDto
    {
        public long UserId { get; init; }
        public int RatingValue { get; set; }
        public string Comment {  get; set; }
        public DateTime RatingTime {  get; set; }
    }
}
