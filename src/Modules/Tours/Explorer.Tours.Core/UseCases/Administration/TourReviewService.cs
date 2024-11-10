using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;

namespace Explorer.Tours.Core.UseCases.Administration;

public class TourReviewService : CrudService<TourReviewDto, TourReview>, ITourReviewService
{
    public TourReviewService(ICrudRepository<TourReview> repository, IMapper mapper) : base(repository, mapper) { }
}
