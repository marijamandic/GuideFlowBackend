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
        try
        {
            if (problem == null) throw new ArgumentNullException(nameof(problem));
            var res = MapToDomain(problem);
        }
        catch (Exception ex)
        {
            
            throw;
        }
        var result = _problemRepository.Create(MapToDomain(problem));
        return MapToDto(result);
    }

    public Result<PagedResult<ProblemDto>> GetAll()
    {
        var problems = _problemRepository.GetAll();
        return MapToDto(problems);
    }
    public Result<PagedResult<ProblemDto>> GetUserProblems(int userId)
    {
        var problems = _problemRepository.GetUserProblems(userId);
        return MapToDto(problems);
    }

    public Result<ProblemDto> Update(ProbStatusChangeDto status, int id)
    {
        var problem = _problemRepository.GetById(id);
        problem.ChangeStatus(status.TouristMessage, status.IsSolved);
        var result = _problemRepository.Update(problem);
        return MapToDto(result);
    }
    public Result<ProblemDto> UpdateDeadline(int id,DateTime deadline) 
    {
        var problem = _problemRepository.GetById(id);
        problem.ChangeDeadline(deadline);
        var result = _problemRepository.Update(problem);
        return MapToDto(result);
    }
}
