using Explorer.Payments.API.Public;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Author;
using FluentResults;
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
        private readonly ITourPurchaseTokenService _tourPurchaseTokenService;
        private readonly IPaymentService _paymentService;

        public AuthorDashboardService(ITourReviewService tourReviewService, ITourService tourService, ITourPurchaseTokenService tourPurchaseTokenService, IPaymentService paymentService)
        {
            _tourReviewService = tourReviewService;
            _tourService = tourService;
            _tourPurchaseTokenService = tourPurchaseTokenService;
            _paymentService = paymentService;
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


        public Result<int> GetNumberOfPublishedTours(int authorId)
        {

            var toursResult = _tourService.GetTourIdsByAuthorId(authorId);

            if (!toursResult.IsSuccess)
            {
                return Result.Fail<int>("Failed to retrieve tours for the author.");
            }

            var tours = toursResult.Value;
            int total = 0;

            foreach (var tourId in tours)
            {
                var tourResult = _tourService.Get(Convert.ToInt32(tourId));
                if (!tourResult.IsSuccess || tourResult.Value == null)
                {
                    continue; 
                }

                if (tourResult.Value.Status == TourStatus.Published)
                {
                    total++;
                }
            }

            return Result.Ok(total);
        }

        public Result<int> GetNumberOfPurchashedTours(int authorId)
        {
            int total = 0;
            var toursResult = _tourService.GetTourIdsByAuthorId(authorId);

            if (toursResult.Value == null || !toursResult.Value.Any())
            {
                return Result.Fail<int>("No tours found or failed to fetch tours.");
            }

            var tours = toursResult.Value;           

            foreach (var tourId in tours)
            {
                int purchases = _tourPurchaseTokenService.GetNumOfPurchases(tourId);
                total += purchases;
            }
            return Result.Ok(total);
        }

        public Result<int> GetTotalSales(int authorId)
        {
            int total = 0;
            var toursResult = _tourService.GetTourIdsByAuthorId(authorId);

            if (toursResult.Value == null || !toursResult.Value.Any())
            {
                return Result.Fail<int>("No tours found or failed to fetch tours.");
            }

            var tours = toursResult.Value;

            foreach (var tourId in tours)
            {
                var tourResult = _tourService.Get(Convert.ToInt32(tourId));
                
                int purchases = _tourPurchaseTokenService.GetNumOfPurchases(tourId);
                total += purchases * tourResult.Value.Price;
            }
            return Result.Ok(total);
        }
    }
}
