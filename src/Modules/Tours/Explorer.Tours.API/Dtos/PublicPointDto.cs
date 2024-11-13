using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class PublicPointDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string ImageUrl { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public PointType PointType { get; set; }
        public int AuthorId { get; set; }
    }

    public enum ApprovalStatus
    {
        Pending,    
        Accepted,   
        Rejected
    }

    public enum PointType
    {
        Checkpoint,
        Object
    }
}
