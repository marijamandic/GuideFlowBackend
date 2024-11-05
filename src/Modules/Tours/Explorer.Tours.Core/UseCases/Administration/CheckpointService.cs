using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration;

public class CheckpointService : CrudService<CheckpointDto, Checkpoint>, ICheckpointService
{

    private readonly ICheckpointRepository _checkpointRepository;
    private readonly IMapper _mapper; // Injektovanje mappera

    public CheckpointService(ICrudRepository<Checkpoint> repository, IMapper mapper, ICheckpointRepository checkpointRepository) : base(repository, mapper) {
        _checkpointRepository = checkpointRepository;
        _mapper = mapper;
    }

    public List<CheckpointDto> GetCheckpointsByTour(int tourId)
    {
        var checkpoints = _checkpointRepository.GetByTour(tourId);
        return _mapper.Map<List<CheckpointDto>>(checkpoints); // Mapiranje
    }

}