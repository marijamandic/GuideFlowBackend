using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases;
public class ProblemService : BaseService<ProblemDto, Problem>, IProblemService
{
    private readonly IProblemRepository _problemRepository;

    public ProblemService(IProblemRepository problemRepository, IMapper mapper) : base(mapper)
    {
        _problemRepository = problemRepository;
    }

    public Result<ProblemDto> Create(ProblemDto problem)
    {
        var result = _problemRepository.Create(MapToDomain(problem));
        return MapToDto(result);
    }

    public Result<PagedResult<ProblemDto>> GetPaged(int page, int pageSize)
    {
        var result = _problemRepository.GetPaged(page, pageSize);
        return MapToDto(result);
    }
}
