using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos.Problems;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.Problems;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases;
public class ProblemService : BaseService<ProblemDto, Problem>, IProblemService
{
    private readonly IProblemRepository _problemRepository;

    public ProblemService(IMapper mapper, IProblemRepository problemRepository) : base(mapper)
    {
        _problemRepository = problemRepository;
    }

    public Result<ProblemDto> Create(ProblemDto problem)
    {
        var result = _problemRepository.Create(MapToDomain(problem));
        return MapToDto(result);
    }

    public Result<PagedResult<ProblemDto>> GetAll()
    {
        var problems = _problemRepository.GetAll();
        return MapToDto(problems);
    }
}
