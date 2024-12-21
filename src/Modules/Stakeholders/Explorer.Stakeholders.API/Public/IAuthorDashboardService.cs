using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IAuthorDashboardService
    {
        double GetAverageGradeForAuthor(long authorId);
        Result<TourDto> GetBestSellingTourByAuthorId(int id);
        Result<TourDto> GetWorstSellingTourByAuthorId(int id);
        Dictionary<int, int> GetReviewsPartitionedByGrade(long authorId);
        Result<TourDto> GetLowestRatedTourByAuthorId(int id);
        Dictionary<DateTime, int> GetTourPaymentsForNumOfMonths(int authorId, int months);

    }
}
