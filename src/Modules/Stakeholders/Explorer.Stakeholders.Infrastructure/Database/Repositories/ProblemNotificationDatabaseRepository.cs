﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories;

public class ProblemNotificationDatabaseRepository : IProblemNotificationRepository
{
    private readonly StakeholdersContext _stakeholdersContext;
    private readonly DbSet<ProblemNotification> _notifications;

    public ProblemNotificationDatabaseRepository(StakeholdersContext stakeholdersContext)
    {
        _stakeholdersContext = stakeholdersContext;
        _notifications = _stakeholdersContext.Set<ProblemNotification>();
    }


    public void Create(ProblemNotification notification)
    {
        _notifications.Add(notification);
        _stakeholdersContext.SaveChanges();
    }

    public PagedResult<ProblemNotification> GetByUserId(long userId)
    {
        var notifications = _notifications
            .Where(n => n.UserId == userId)
            .ToList();
        return new PagedResult<ProblemNotification>(notifications, notifications.Count());
    }
}
