using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos.Problems;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.Problems;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases;
public class ProblemService : CrudService<ProblemDto, Problem>, IProblemService
{
    private readonly IProblemRepository _problemRepository;

    public ProblemService(ICrudRepository<Problem> crudRepository, IMapper mapper, IProblemRepository problemRepository) : base(crudRepository, mapper)
    {
        _problemRepository = problemRepository;
    }

    public Result<PagedResult<ProblemDto>> GetAll()
    {
        var problems = _problemRepository.GetAll();
        return MapToDto(problems);
    }
}
