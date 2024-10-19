using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using System.Collections.Generic;
using FluentResults;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;

namespace Explorer.Tours.Core.UseCases
{
    public class TourSpecificationService : CrudService<TourSpecificationDto, TourSpecifications>, ITourSpecificationService
    {
        private readonly ITourSpecificationRepository _tourSpecificationRepository;
        private readonly List<TourSpecificationDto> _tourSpecifications;
        private readonly ICrudRepository<TourSpecifications> _crudRepository;
        private readonly IMapper _mapper;
        public TourSpecificationService(ICrudRepository<TourSpecifications> crudRepository, IMapper mapper, ITourSpecificationRepository tourSpecificationRepository) : base(crudRepository, mapper)
        {
            _crudRepository = crudRepository;
            _mapper = mapper;
            _tourSpecificationRepository = tourSpecificationRepository;
        }

        public IEnumerable<TourSpecificationDto> GetAllTourSpecifications_()
        {
            var tourSpecs = _tourSpecificationRepository.GetAll();
            var tourSpecificationDtos = tourSpecs.Select(t => _mapper.Map<TourSpecificationDto>(t));
            return tourSpecificationDtos;
        }

        public Result<TourSpecificationDto> CreateTourSpecifications(TourSpecificationDto tourSpecificationDto)
        {
            var allSpec = GetAllTourSpecifications_();
            var existingTourSpec = allSpec
                .FirstOrDefault(t => t.UserId == tourSpecificationDto.UserId);

            if (existingTourSpec != null)
            {
                throw new ArgumentException("Specifikacija ture za ovog korisnika već postoji.");
            }

            allSpec.ToList().Add(tourSpecificationDto);

            return tourSpecificationDto;
        }

        public Result<IEnumerable<TourSpecificationDto>> GetAllTourSpecifications()
        {
            return _tourSpecifications;
        }

        public Result UpdateTourSpecifications(TourSpecificationDto tourSpecificationDto)
        {
            var allSpec = GetAllTourSpecifications_();
            var existingTourSpec = allSpec
                .FirstOrDefault(t => t.UserId == tourSpecificationDto.UserId);

            if (existingTourSpec != null)
            {
                existingTourSpec.TourDifficulty = tourSpecificationDto.TourDifficulty;
                existingTourSpec.Tags = tourSpecificationDto.Tags;
                existingTourSpec.BikeRating = tourSpecificationDto.BikeRating;
                existingTourSpec.BoatRating = tourSpecificationDto.BoatRating;
                existingTourSpec.CarRating = tourSpecificationDto.CarRating;
                existingTourSpec.WalkRating = tourSpecificationDto.WalkRating;
                return Result.Ok();

            }
            else
            {
                return Result.Fail("Specifikacija ture nije pronađena.");

            }
        }

        public Result DeleteTourSpecifications(long userId)
        {
            var allSpec = GetAllTourSpecifications_();
            var tourSpec = allSpec.FirstOrDefault(t => t.UserId == userId);

            if (tourSpec != null)
            {
                _tourSpecifications.Remove(tourSpec);
                return Result.Ok();
            }
            else
            {
                return Result.Fail("Specifikacija ture nije pronađena.");

            }
        }

        
        public Result<TourSpecificationDto> GetTourSpecificationsByUserId(long userId)
        {
            var allSpec = GetAllTourSpecifications_();
            var tourSpec = allSpec.FirstOrDefault(t => t.UserId == userId);

            if (tourSpec != null)
            {
                return Result.Ok(tourSpec);
            }
            else
            {
                return Result.Fail("Specifikacija ture nije pronađena za zadatog korisnika.");
            }
        }
    }
}
