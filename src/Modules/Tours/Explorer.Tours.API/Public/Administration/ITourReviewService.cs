using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Author;
using FluentResults;

namespace Explorer.Tours.API.Public.Administration;

public interface ITourReviewService
{
    Result<PagedResult<TourReviewDto>> GetPaged(int page, int pageSize);
    Result<TourReviewDto> Create(TourReviewDto tourReview);
    Result<TourReviewDto> Update(TourReviewDto tourReview);
    Result Delete(int id);
    IEnumerable<TourReviewDto> GetReviewsByAuthorId(long authorId, ITourService tourService);
    double GetAvgGradeByTourId(long tourId);
}
