using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain;
public class TourReview : Entity
{
    public int Rating { get; init; }
    public string? Comment { get; init; }
    public DateTime TourDate { get; init; }
    public DateTime CreationDate { get; init; }

    public TourReview(int rating, string comment, DateTime tourDate, DateTime creationDate)
    {
        Rating = rating;
        Comment = comment;
        TourDate = tourDate;
        CreationDate = creationDate;
    }
}
