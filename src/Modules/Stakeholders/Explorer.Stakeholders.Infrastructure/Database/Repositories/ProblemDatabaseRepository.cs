﻿using Explorer.BuildingBlocks.Core.UseCases;
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

    public Problem GetById(long id)
    {
        var result = _problems
            .Include(p => p.Messages)
            .FirstOrDefault(p => p.Id == id);

        return result!;
    }

    public PagedResult<Problem> GetByTourIds(List<long> tourIds)
    {
        var result = _problems
            .Where(p => tourIds.Contains(p.TourId))
            .Include(p => p.Messages).ToList();
        return new PagedResult<Problem>(result, result.Count);
    }

    public Problem Save(Problem problem)
    {
        _stakeholdersContext.Entry(problem).State = EntityState.Modified;
        _stakeholdersContext.SaveChanges();
        return problem;
    }

    public PagedResult<Problem> GetByTouristId(long touristId)
    {
        var result = _problems
            .Where(p => p.UserId == touristId)
            .Include(p => p.Messages)
            .ToList();
        return new PagedResult<Problem> (result, result.Count);
    }
}
