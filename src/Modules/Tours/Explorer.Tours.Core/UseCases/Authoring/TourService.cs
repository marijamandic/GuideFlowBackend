using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Author;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Authoring
{
    public class TourService : BaseService<TourDto, Tour>, ITourService
    {
        private readonly ITourRepository tourRepository;
        private readonly IMapper mapper;
        public TourService(ITourRepository tourRepository, IMapper mapper) : base(mapper) 
        { 
            this.tourRepository=tourRepository;
            this.mapper=mapper;
        }

        public Result<PagedResult<TourDto>> GetPaged(int page, int pageSize)
        {
            var result = tourRepository.GetPaged(page, pageSize);
            return MapToDto(result);
        }

        public Result<TourDto> Get(int id)
        {
            try
            {
                var result = tourRepository.Get(id);
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<TourDto> Create(TourDto entity)
        {
            try
            {
                var result = tourRepository.Create(MapToDomain(entity));
                return MapToDto(result);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<TourDto> Update(TourDto entity)
        {
            try
            {
                var result = tourRepository.Update(MapToDomain(entity));
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }


        public Result Delete(int id)
        {
            try
            {
                tourRepository.Delete(id);
                return Result.Ok();
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<TourDto> AddCheckpoint(int Id, CheckpointDto checkpoint, double updatedLength)
        {
            try
            {
                Tour tour = tourRepository.Get(Id);
                tour.AddCheckpoint(mapper.Map<Checkpoint>(checkpoint), updatedLength);
                var result = tourRepository.Update(tour);
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<TourDto> AddTransportDurations(int id, List<TransportDurationDto> transportDurations)
        {
            try
            {
                Tour tour = tourRepository.Get(id);
                tour.AddTransportDuratios(transportDurations.Select(dto => mapper.Map<TransportDuration>(dto)).ToList());
                var result = tourRepository.Update(tour);
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<TourDto> Archive(int id)
        {
            try
            {
                Tour tour = tourRepository.Get(id);
                tour.Archive();
                var result = tourRepository.Update(tour);
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        

        public Result<TourDto> Publish(int id)
        {
            try
            {
                var tour = tourRepository.Get(id);
                tour.ChangeStatusToPublish();
                var updatedTour = tourRepository.Update(tour);
                return MapToDto( updatedTour);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public List<TourDto> SearchTours(double lat, double lon, double distance, int page, int pageSize)
        {
            /*try
            {*/
                var publishedTours = tourRepository.GetPaged(page, pageSize).Results.Where(t => t.Status == Domain.Tours.TourStatus.Published).ToList();
                var matchingTours = new List<TourDto>();
                foreach (var tour in publishedTours)
                {
                    foreach (var checkpoint in tour.Checkpoints)
                    {
                        double distanceToCheckpoint = CalculateDistance(lat, lon, checkpoint.Latitude, checkpoint.Longitude);
                        if (distanceToCheckpoint <= distance)
                            matchingTours.Add(MapToDto(tour));
                        break;
                    }
                }
                return matchingTours;
            /*}
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }*/
        }

        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371; // Radius of the Earth in kilometers
            double dLat = DegreesToRadians(lat2 - lat1);
            double dLon = DegreesToRadians(lon2 - lon1);
            double a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c; // Distance in kilometers
        }

        private double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

    }
}
