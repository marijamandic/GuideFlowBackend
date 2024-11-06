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

    public Problem Create(Problem problem)
    {
        _problems.Add(problem);
        _stakeholdersContext.SaveChanges();
        return problem;
    }

    public PagedResult<Problem> GetAll()
    {
        var result = _problems.Include(p => p.Messages).ToList();
        return new PagedResult<Problem>(result, result.Count);
    }
    public Problem Update(Problem problem)
    {
        _stakeholdersContext.Entry(problem).State = EntityState.Modified;
        _stakeholdersContext.SaveChanges();
        return problem;
    }
    public Problem GetById(int id)
    {
        var problem = _stakeholdersContext.Problems.FirstOrDefault(p => p.Id == id);
        if(problem == null) throw new KeyNotFoundException($"Problem with ID {id} not found.");
        return problem;
    }
    public PagedResult<Problem> GetUserProblems(int userId)
    {
        var result = _problems.Include(p => p.Messages).Where(i => i.UserId == userId).ToList();
        return new PagedResult<Problem>(result,result.Count);
    }
}
