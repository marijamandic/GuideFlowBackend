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

        public Result<TourDto> GetBestSellingTourByAuthorId(int id)
        {
            try
            {
                var toursResult = _tourService.GetTourIdsByAuthorId(id);

                if (toursResult.Value == null || !toursResult.Value.Any())
                {
                    return Result.Fail<TourDto>("No tours found or failed to fetch tours.");
                }

                var tours = toursResult.Value;

                long bestTourId = -1;
                int bestNumber = 0;
                foreach (var tourId in tours)
                {
                    int purchases = _tourPurchaseTokenService.GetNumOfPurchases(tourId);
                    if (purchases >= bestNumber)
                    {
                        bestNumber = purchases;
                        bestTourId = tourId;
                    }
                }

                var bestTourResult = _tourService.Get(Convert.ToInt32(bestTourId));

                if (bestTourId == -1)
                {
                    return Result.Fail<TourDto>("Korisnik nema validnih tura za analizu.");
                }


                if (bestTourResult.IsFailed)
                {
                    return Result.Fail<TourDto>("Failed to retrieve the best-selling tour.");
                }

                var bestTour = bestTourResult.Value;

                return Result.Ok(bestTour);
            }
            catch (Exception ex)
            {
                return Result.Fail<TourDto>(ex.Message);
            }
        }

        public Result<TourDto> GetWorstSellingTourByAuthorId(int id)
        {
            try
            {
                var toursResult = _tourService.GetTourIdsByAuthorId(id);

                if (toursResult.Value == null || !toursResult.Value.Any())
                {
                    return Result.Fail<TourDto>("No tours found or failed to fetch tours.");
                }

                var tours = toursResult.Value;

                long worstTourId = tours.First();
                int worstNumber = _tourPurchaseTokenService.GetNumOfPurchases(worstTourId);

                foreach (var tourId in tours)
                {
                    int purchases = _tourPurchaseTokenService.GetNumOfPurchases(tourId);
                    if (purchases < worstNumber)
                    {
                        worstNumber = purchases;
                        worstTourId = tourId;
                    }
                }

                var worstTourResult = _tourService.Get(Convert.ToInt32(worstTourId));

                if (worstTourResult.IsFailed)
                {
                    return Result.Fail<TourDto>("Failed to retrieve the worst-selling tour.");
                }

                var worstTour = worstTourResult.Value;

                return Result.Ok(worstTour);
            }
            catch (Exception ex)
            {
                return Result.Fail<TourDto>(ex.Message);
            }
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

        public Result<TourDto> GetLowestRatedTourByAuthorId(int id)
        {
            try
            {
                var toursResult = _tourService.GetTourIdsByAuthorId(id);

                if (toursResult.Value == null || !toursResult.Value.Any())
                {
                    return Result.Fail<TourDto>("No tours found or failed to fetch tours.");
                }

                var tours = toursResult.Value;

                long bestTourId = -1;
                double bestRating = 0;
                foreach (var tourId in tours)
                {
                    var tourResult = _tourService.Get(Convert.ToInt32(tourId));
                    if (tourResult.Value.AverageGrade > bestRating)
                    {
                        bestRating = tourResult.Value.AverageGrade;
                        bestTourId = tourId;
                    }
                }

                var bestTourResult = _tourService.Get(Convert.ToInt32(bestTourId));

                if (bestTourId == -1)
                {
                    return Result.Fail<TourDto>("Korisnik nema validnih recenzija za analizu.");
                }


                if (bestTourResult.IsFailed)
                {
                    return Result.Fail<TourDto>("Failed to retrieve the best-selling tour.");
                }

                var bestTour = bestTourResult.Value;

                return Result.Ok(bestTour);
            }
            catch (Exception ex)
            {
                return Result.Fail<TourDto>(ex.Message);
            }
        }

        public Dictionary<DateTime, int> GetTourPaymentsForNumOfMonths(int authorId, int months)
        {
            try
            {
                var toursResult = _tourService.GetTourIdsByAuthorId(authorId);

                if (toursResult.Value == null || !toursResult.Value.Any())
                {
                    return null;
                }
                Dictionary<DateTime, int> dictionary = _paymentService.GetTourPaymentsWithProductIds(months, toursResult.Value);
                return dictionary;
            }
            catch (Exception ex)
            {
                return null;
            }
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
