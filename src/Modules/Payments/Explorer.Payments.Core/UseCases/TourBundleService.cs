using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases
{
    public class TourBundleService : BaseService<TourBundleDto, TourBundle>, ITourBundleService
    {
        private readonly ITourBundleRepository _tourBundleRepository;

        public TourBundleService(IMapper mapper, ITourBundleRepository tourBundleRepository) : base(mapper)
        {
            _tourBundleRepository = tourBundleRepository;
        }

        public Result<TourBundleDto> Create(TourBundleDto tourBundleDto)
        {
            _tourBundleRepository.Create(MapToDomain(tourBundleDto));
            return tourBundleDto;
        }

        public Result<TourBundleDto> Delete(long tourBundleId)
        {
            try
            {
                var tourBundle = _tourBundleRepository.GetById(tourBundleId);
                if (tourBundle.Status == Domain.BundleStatus.Published)
                    return Result.Fail(FailureCode.InvalidArgument).WithError("Cannot Delete Published Tour Bundle");
                _tourBundleRepository.Delete(tourBundle);
                _tourBundleRepository.Save(tourBundle);
                return MapToDto(tourBundle);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<TourBundleDto> AddTour(long tourBundleId, long tourId)
        {
            try
            {
                var tourBundle = _tourBundleRepository.GetById(tourBundleId);
                tourBundle.AddTour(tourId);
                _tourBundleRepository.Save(tourBundle);
                return MapToDto(tourBundle);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<TourBundleDto> RemoveTour(long tourBundleId, long tourId)
        {
            try
            {
                var tourBundle = _tourBundleRepository.GetById(tourBundleId);
                tourBundle.RemoveTour(tourId);
                _tourBundleRepository.Save(tourBundle);
                return MapToDto(tourBundle);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Result.Fail(FailureCode.Internal).WithError(e.Message);
            }
        }

        public Result<TourBundleDto> Publish(long tourBundleId)
        {
            try
            {
                var tourBundle = _tourBundleRepository.GetById(tourBundleId);
                tourBundle.Publish();
                _tourBundleRepository.Save(tourBundle);
                return MapToDto(tourBundle);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<TourBundleDto> Archive(long tourBundleId)
        {
            try
            {
                var tourBundle = _tourBundleRepository.GetById(tourBundleId);
                tourBundle.Archive();
                _tourBundleRepository.Save(tourBundle);
                return MapToDto(tourBundle);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

    }
}