using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories;

public class NotificationDatabaseRepository : INotificationRepository
{
    private readonly StakeholdersContext _stakeholdersContext;
    private readonly DbSet<ProblemNotification> _notifications;

    public NotificationDatabaseRepository(StakeholdersContext stakeholdersContext)
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
            .Where(n => (n.UserId == userId) && (n.Type == NotificationType.ProblemNotification))
            .ToList();
        return new PagedResult<ProblemNotification>(notifications, notifications.Count());
    }

    public ProblemNotification GetById(long id)
    {
        var notification = _notifications.FirstOrDefault(n => n.Id == id);
        return notification == null ? throw new ArgumentException("Invalid notification ID.") : notification;
    }

    public ProblemNotification Save(ProblemNotification notification)
    {
        try
        {
            _stakeholdersContext.Update(notification);
            _stakeholdersContext.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            throw new KeyNotFoundException(e.Message);
        }
        return notification;
    }
}
