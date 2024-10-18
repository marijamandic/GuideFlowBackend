using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories;
public class ProblemRepository : IProblemRepository
{
    private readonly StakeholdersContext _context;
    private readonly DbSet<Problem> _dbSet;

    public ProblemRepository(StakeholdersContext context)
    {
        _context = context;
        _dbSet = _context.Set<Problem>();
    }

    public Problem Create(Problem problem)
    {
        _dbSet.Add(problem);
        _context.SaveChanges();
        return problem;
    }

    public PagedResult<Problem> GetPaged(int page, int pageSize)
    {
        var task = _dbSet.GetPaged(page, pageSize);
        task.Wait();
        return task.Result;
    }
}
