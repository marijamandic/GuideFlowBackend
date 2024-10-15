using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using System.Collections.Generic;
using FluentResults;
using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Tours.Core.UseCases
{
    public class TourSpecificationService : CrudService<TourSpecificationDto, TourSpecifications>, ITourSpecificationService
    {
        private readonly List<TourSpecificationDto> _tourSpecifications;

        public TourSpecificationService(ICrudRepository<TourSpecifications> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {
            _tourSpecifications = new List<TourSpecificationDto>();
        }

        public Result<TourSpecificationDto> CreateTourSpecifications(TourSpecificationDto tourSpecificationDto)
        {
            var existingTourSpec = _tourSpecifications
                .FirstOrDefault(t => t.UserId == tourSpecificationDto.UserId);

            if (existingTourSpec != null)
            {
                throw new ArgumentException("Specifikacija ture za ovog korisnika već postoji.");
            }

            _tourSpecifications.Add(tourSpecificationDto);

            return tourSpecificationDto;
        }

        public Result<IEnumerable<TourSpecificationDto>> GetAllTourSpecifications()
        {
            return _tourSpecifications;
        }

        public Result UpdateTourSpecifications(TourSpecificationDto tourSpecificationDto)
        {
            var existingTourSpec = _tourSpecifications
                .FirstOrDefault(t => t.UserId == tourSpecificationDto.UserId);

            if (existingTourSpec != null)
            {
                existingTourSpec.TourDifficulty = tourSpecificationDto.TourDifficulty;
                existingTourSpec.Tags = tourSpecificationDto.Tags;
                return Result.Ok();

            }
            else
            {
                return Result.Fail("Specifikacija ture nije pronađena.");

            }
        }

        public Result DeleteTourSpecifications(long userId)
        {
            var tourSpec = _tourSpecifications.FirstOrDefault(t => t.UserId == userId);

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
            var tourSpec = _tourSpecifications.FirstOrDefault(t => t.UserId == userId);

            if (tourSpec != null)
            {
                return tourSpec;
            }
            else
            {
                throw new ArgumentException("Specifikacija ture nije pronađena za zadatog korisnika.");
            }
        }
    }
}
