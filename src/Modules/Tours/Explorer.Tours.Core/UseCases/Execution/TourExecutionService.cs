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
        private readonly ICheckpointService _checkpointService;
        private readonly IMapper _mapper;
        public TourExecutionService(IMapper mapper , ITourExecutionRepository tourExecutionRepository , ITourService tourService , ICheckpointService checkpointService) : base(mapper) {
            _tourExecutionRepository = tourExecutionRepository;
            _tourService = tourService;
            _checkpointService = checkpointService;
            _mapper = mapper;
        }
        public Result<TourExecutionDto> Create(CreateTourExecutionDto createTourExecutionDto) {
            TourDto tour = _tourService.Get(createTourExecutionDto.TourId).Value;

            var tourExecution = new TourExecution(tour.Id,createTourExecutionDto.UserId,500);
            tourExecution.AddCheckpointStatuses(tour.Checkpoints.Select(c => _mapper.Map<Checkpoint>(c)).ToList());

            _tourExecutionRepository.Create(tourExecution);
            return MapToDto(tourExecution);
        }
        public Result<TourExecutionDto> Update(UpdateTourExecutionDto updateTourExecutionDto) {
            var tourExecution = _tourExecutionRepository.Get(updateTourExecutionDto.TourExecutionId);
            tourExecution.UpdateLocation(updateTourExecutionDto.Longitude, updateTourExecutionDto.Latitude);

            _tourExecutionRepository.Update(tourExecution);
            return MapToDto(tourExecution);
        }

        public Result<TourExecutionDto> GetSessionsByUserId(long userId)
        {
            var sessions = _tourExecutionRepository.GetByUserId(userId);
            return MapToDto(sessions);   
        }

        public void CompleteSession(long userId)
        {
            var tourExecution = _tourExecutionRepository.GetByUserId(userId);

            if (tourExecution == null)
            {
                throw new Exception("Tour execution not found.");
            }

            if (tourExecution.CheckpointsStatus.All(c => c.IsCompleted()))
            {
                tourExecution.CompleteSession();
                _tourExecutionRepository.Update(tourExecution);
            }
            else
            {
                throw new InvalidOperationException("All checkpoints must be passed to complete the tour.");
            }
        }

        public void AbandonSession(long userId)
        {
            var tourExecution = _tourExecutionRepository.GetByUserId(userId);

            if (tourExecution == null)
            {
                throw new Exception("Tour execution not found.");
            }

            tourExecution.AbandonSession();
            _tourExecutionRepository.Update(tourExecution);
        }



    }
}
