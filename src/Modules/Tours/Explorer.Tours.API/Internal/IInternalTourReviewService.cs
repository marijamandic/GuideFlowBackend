using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Author;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Internal
{
    public interface IInternalTourReviewService
    {
        double GetAvgGradeByTourId(long tourId);
        IEnumerable<TourReviewDto> GetReviewsByAuthorId(long authorId, ITourService tourService);
        Dictionary<int, int> GetReviewsPartitionedByGrade(long authorId);
    }

}
