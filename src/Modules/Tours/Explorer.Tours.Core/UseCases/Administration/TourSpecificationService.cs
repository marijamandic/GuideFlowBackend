using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
using System.Collections.Generic;
using FluentResults;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.Tours;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class TourSpecificationService : CrudService<TourSpecificationDto, TourSpecification>, ITourSpecificationService
    {
        private readonly ITourSpecificationRepository _tourSpecificationRepository;
        private readonly List<TourSpecificationDto> _tourSpecifications;
        private readonly IMapper _mapper;
        private readonly ITourRepository _tourRepository;
        public TourSpecificationService(ICrudRepository<TourSpecification> crudRepository, IMapper mapper, ITourSpecificationRepository tourSpecificationRepository, ITourRepository tourRepository) : base(crudRepository, mapper)
        {
            _mapper = mapper;
            _tourSpecificationRepository = tourSpecificationRepository;
            _tourRepository = tourRepository;
        }

        public Result<TourSpecificationDto> Create(TourSpecificationDto tourSpecificationDto)
        {
            var tourSpecification = new TourSpecification(
                tourSpecificationDto.UserId,
                tourSpecificationDto.Level,
                tourSpecificationDto.Taggs
            );

            _tourSpecificationRepository.Create(tourSpecification);

            var transportRatings = tourSpecificationDto.TransportRatings.Select(dto => new TransportRating(tourSpecification.Id, dto.Rating, dto.TransportMode)).ToList();

            _tourSpecificationRepository.AddTransportRatings(tourSpecification.Id, transportRatings);

            var tourSpecificationDtoResult = new TourSpecificationDto
            {
                Id = tourSpecification.Id,
                UserId = tourSpecification.UserId,
                Level = tourSpecification.Level,
                Taggs = tourSpecification.Taggs,
                TransportRatings = transportRatings.Select(rating => new TransportRatingDto(rating.TransportationMode, rating.Rating)).ToList()
            };

            return Result.Ok(tourSpecificationDtoResult);
        }

        public Result<TourSpecificationDto> GetByUserId(long userId)
        {
            var tourSpecification = _tourSpecificationRepository.GetByUserId(userId);

            if(tourSpecification == null)
                return Result.Fail("Tour specification not found for this user");

            var tourSpecificationDtoResult = new TourSpecificationDto
            {
                Id = tourSpecification.Id,
                UserId = tourSpecification.UserId,
                Level = tourSpecification.Level,
                Taggs = tourSpecification.Taggs,
                TransportRatings = tourSpecification.TransportRatings.Select(rating => new TransportRatingDto(rating.TransportationMode, rating.Rating)).ToList()
            };

            return Result.Ok(tourSpecificationDtoResult);
        }

        public Result AddTransportRating(long tourSpecificationId, IEnumerable<TransportRatingDto> transportRatingsDto)
        {
            var transportRatings = transportRatingsDto.Select(dto => new TransportRating(tourSpecificationId, dto.Rating, dto.TransportMode)).ToList();

            _tourSpecificationRepository.AddTransportRatings(tourSpecificationId, transportRatings);
            return Result.Ok();
        }

    }
}
