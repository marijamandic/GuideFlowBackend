using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Internal;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Author;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Core.UseCases.Authoring;

namespace Explorer.Tours.Core.UseCases.Administration;

public class TourReviewService : CrudService<TourReviewDto, TourReview>, ITourReviewService, IInternalTourReviewService
{

    private readonly ICrudRepository<TourReview> _repository;
    private readonly ITourService _tourService;

    public TourReviewService(ICrudRepository<TourReview> repository, ITourService tourService, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
        _tourService = tourService;
    }

    public double GetAvgGradeByTourId(long tourId)
    {
        var reviews = GetPagedReviews().Where(r => r.TourId == tourId).ToList();
        return reviews.Any() ? reviews.Average(r => r.Rating) : 0.0;
    }

    public IEnumerable<TourReviewDto> GetReviewsByAuthorId(long authorId, ITourService tourService)
    {
        var tourIds = tourService.GetTourIdsByAuthorId((int)authorId).Value;

        if (tourIds == null || !tourIds.Any())
        {
            return Enumerable.Empty<TourReviewDto>();
        }

        return GetPagedReviews()
               .Where(r => tourIds.Contains(r.TourId))
               .Select(MapToDto);
    }

    public Dictionary<int, int> GetReviewsPartitionedByGrade(long authorId)
    {
        var tourIdsResult = _tourService.GetTourIdsByAuthorId((int)authorId);

        if (!tourIdsResult.IsSuccess || tourIdsResult.Value == null || !tourIdsResult.Value.Any())
        {
            return new Dictionary<int, int>(); // No tours found for the author
        }

        var tourIds = tourIdsResult.Value;

        var reviews = GetPagedReviews()
            .Where(r => tourIds.Contains(r.TourId))
            .ToList();

        return reviews.GroupBy(r => r.Rating)
                      .ToDictionary(g => g.Key, g => g.Count());
    }

    private IEnumerable<TourReview> GetPagedReviews()
    {
        const int pageSize = 100;
        int currentPage = 1;
        var allReviews = new List<TourReview>();

        while (true)
        {
            var pagedResult = _repository.GetPaged(currentPage, pageSize);

            if (pagedResult.Results == null || !pagedResult.Results.Any())
                break;

            allReviews.AddRange(pagedResult.Results);

            if (pagedResult.Results.Count < pageSize)
                break;

            currentPage++;
        }

        return allReviews;
    }

    private TourReviewDto MapToDto(TourReview review)
    {
        return new TourReviewDto
        {
            Id = (int)review.Id, // Explicit cast to int
            Rating = review.Rating,
            Comment = review.Comment,
            TourDate = review.TourDate,
            CreationDate = review.CreationDate,
            PercentageCompleted = review.PercentageCompleted,
            TouristId = (int)review.TouristId, // Explicit cast to int
            TourId = (int)review.TourId // Explicit cast to int
        };
    }

}
