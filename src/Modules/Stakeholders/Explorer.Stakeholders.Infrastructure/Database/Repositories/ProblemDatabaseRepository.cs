using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain.Problems;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories;
public class ProblemDatabaseRepository : IProblemRepository
{
    private readonly StakeholdersContext _stakeholdersContext;
    private readonly DbSet<Problem> _problems;

    public ProblemDatabaseRepository(StakeholdersContext stakeholdersContext)
    {
        _stakeholdersContext = stakeholdersContext;
        _problems = _stakeholdersContext.Set<Problem>();
    }

    public PagedResult<Problem> GetAll()
    {
        var result = _problems.Include(p => p.Messages).ToList();
        return new PagedResult<Problem>(result, result.Count);
    }
}
