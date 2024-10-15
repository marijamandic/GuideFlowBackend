using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;

namespace Explorer.Tours.Core.UseCases.Administration;

public class CheckpointService : CrudService<CheckpointDto, Checkpoint>, ICheckpointService
{
    public CheckpointService(ICrudRepository<Checkpoint> repository, IMapper mapper) : base(repository, mapper) { }
}