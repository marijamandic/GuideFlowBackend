using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Author;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class AuthorDashboardService : IAuthorDashboardService
    {
        private readonly ITourReviewService _tourReviewService;
        private readonly ITourService _tourService;

        public AuthorDashboardService(ITourReviewService tourReviewService, ITourService tourService)
        {
            _tourReviewService = tourReviewService;
            _tourService = tourService;
        }

        public double GetAverageGradeForAuthor(long authorId)
        {
            var reviews = _tourReviewService.GetReviewsByAuthorId(authorId, _tourService);
            if (reviews == null || !reviews.Any())
            {
                return 0.0; // No reviews, return 0
            }

            return reviews.Average(r => r.Rating);
        }

        public Dictionary<int, int> GetReviewsPartitionedByGrade(long authorId)
        {
            var reviews = _tourReviewService.GetReviewsByAuthorId(authorId, _tourService);
            if (reviews == null || !reviews.Any())
            {
                return new Dictionary<int, int>(); // No reviews, return empty dictionary
            }

            return reviews.GroupBy(r => r.Rating)
                          .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}
