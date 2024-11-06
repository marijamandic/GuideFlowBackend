using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.Execution;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Author;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.TourExecutions;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Execution
{
    public class TourExecutionService : BaseService<TourExecutionDto, TourExecution> , ITourExecutionService
    {
        private readonly ITourExecutionRepository _tourExecutionRepository;
        private readonly ITourService _tourService;
        private readonly IMapper _mapper;
        public TourExecutionService(IMapper mapper , ITourExecutionRepository tourExecutionRepository , ITourService tourService) : base(mapper) {
            _tourExecutionRepository = tourExecutionRepository;
            _tourService = tourService;
            _mapper = mapper;
        }
        public Result<TourExecutionDto> Create(CreateTourExecutionDto createTourExecutionDto) {
                Result<TourDto> tourResult = _tourService.Get(createTourExecutionDto.TourId);
                if (tourResult.IsFailed) return Result.Fail(FailureCode.NotFound);
                TourDto tour = tourResult.Value;
                var tourExecution = new TourExecution(tour.Id, createTourExecutionDto.UserId, tour.LengthInKm);
                tourExecution.AddCheckpointStatuses(tour.Checkpoints.Select(c => _mapper.Map<Checkpoint>(c)).ToList());

                _tourExecutionRepository.Create(tourExecution);
                return MapToDto(tourExecution);
        }
        public Result<TourExecutionDto> Update(UpdateTourExecutionDto updateTourExecutionDto) {
            var tourExecution = _tourExecutionRepository.Get(updateTourExecutionDto.TourExecutionId);
            tourExecution.UpdateLocation(updateTourExecutionDto.Longitude, updateTourExecutionDto.Latitude);
            _tourExecutionRepository.Update(tourExecution);

            var tourExecutionDto = MapToDto(tourExecution);
            SetSecretsForDisplaying(tourExecutionDto);
            return tourExecutionDto;
        }
        public Result<TourExecutionDto> Get(long id)
        {
            var tourExecution = _tourExecutionRepository.Get(id);
            var tourExecutionDto = MapToDto(tourExecution);
            SetSecretsForDisplaying(tourExecutionDto);
            return tourExecutionDto;
        }

        public Result<TourExecutionDto> GetSessionsByUserId(long userId)
        {
            var session = _tourExecutionRepository.GetByUserId(userId);
            var sessionDto = MapToDto(session);
            return sessionDto;   
        }

        public Result<TourExecutionDto> CompleteSession(long userId)
        {
            var tourExecution = _tourExecutionRepository.GetByUserId(userId);

            if (tourExecution == null)
            {
                throw new Exception("Tour execution not found.");
            }

            tourExecution.CompleteSession();
            _tourExecutionRepository.Update(tourExecution);
            var sessionDto = MapToDto(tourExecution);
            return sessionDto;
        }
        public Result<PagedResult<TourExecutionDto>> GetPaged(int page , int pageSize) {
            var result = _tourExecutionRepository.GetPaged(page, pageSize);
            return MapToDto(result);
        }

        public Result<TourExecutionDto> AbandonSession(long userId)
        {
            var tourExecution = _tourExecutionRepository.GetByUserId(userId);

            if (tourExecution == null)
            {
                throw new Exception("Tour execution not found.");
            }

            tourExecution.AbandonSession();
            _tourExecutionRepository.Update(tourExecution);
            var sessionDto = MapToDto(tourExecution);
            return sessionDto;
        }





        #region HelpperMethods
        public void SetSecretsForDisplaying(TourExecutionDto tourExecutionDto)
        {
            foreach (var cs in tourExecutionDto.CheckpointsStatus)
            {
                if (cs.CompletionTime == DateTime.MinValue)
                {
                    var checkpointStatus = tourExecutionDto.CheckpointsStatus.FirstOrDefault(checkpointStatus => cs.Id == checkpointStatus.Id);
                    if (checkpointStatus != null && checkpointStatus.Checkpoint != null)
                    {
                        checkpointStatus.Checkpoint.Secret="Morate da stignete do kljucne tacke da bi ste otkrili tajnu";
                    }
                }
            }
        }
        #endregion
    }
}
