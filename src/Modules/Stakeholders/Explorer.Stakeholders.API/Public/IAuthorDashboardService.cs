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
        Dictionary<int, int> GetReviewsPartitionedByGrade(long authorId);
        Result<int> GetNumberOfPublishedTours(int authorId);

        Result<int> GetNumberOfPurchashedTours(int authorId);
        Result<int> GetTotalSales(int authorId);
        Dictionary<DateTime, int> GetTourPaymentsForNumOfMonths(int authorId, int months);

    }
}
