using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Author;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;

namespace Explorer.Tours.Core.UseCases.Administration;

public class TourReviewService : CrudService<TourReviewDto, TourReview>, ITourReviewService
{

    private readonly ICrudRepository<TourReview> _repository;

    public TourReviewService(ICrudRepository<TourReview> repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
    }

    public IEnumerable<TourReviewDto> GetReviewsByAuthorId(long authorId, ITourService tourService)
    {
        const int pageSize = 100;
        int currentPage = 1;
        var allReviews = new List<TourReviewDto>();

        // Fetch all tour IDs associated with the author
        var tourIdsResult = tourService.GetTourIdsByAuthorId((int)authorId);

        if (!tourIdsResult.IsSuccess || tourIdsResult.Value == null)
        {
            return Enumerable.Empty<TourReviewDto>(); // No tours found for this author
        }

        var tourIds = tourIdsResult.Value;

        // Fetch and filter reviews
        while (true)
        {
            var pagedResult = _repository.GetPaged(currentPage, pageSize);

            if (pagedResult.Results == null || !pagedResult.Results.Any())
                break;

            var filteredReviews = pagedResult.Results
                                             .Where(r => tourIds.Contains(r.TourId))
                                             .Select(r => MapToDto(r));
            allReviews.AddRange(filteredReviews);

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
