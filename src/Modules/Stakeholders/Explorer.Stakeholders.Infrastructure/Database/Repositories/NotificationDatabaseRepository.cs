using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories;

public class NotificationDatabaseRepository : INotificationRepository
{
    private readonly StakeholdersContext _stakeholdersContext;
    private readonly DbSet<ProblemNotification> _problemNotifications;
    private readonly DbSet<Notification> _notifications;

    public NotificationDatabaseRepository(StakeholdersContext stakeholdersContext)
    {
        _stakeholdersContext = stakeholdersContext;
        _problemNotifications = _stakeholdersContext.Set<ProblemNotification>();
        _notifications = _stakeholdersContext.Set<Notification>();
    }
    public void Create(ProblemNotification notification)
    {
        _problemNotifications.Add(notification);
        _stakeholdersContext.SaveChanges();
    }

    public PagedResult<ProblemNotification> GetByUserId(long userId)
    {
        var notifications = _problemNotifications
            .Where(n => (n.UserId == userId) && (n.Type == NotificationType.ProblemNotification))
            .ToList();
        return new PagedResult<ProblemNotification>(notifications, notifications.Count());
    }

    public ProblemNotification GetById(long id)
    {
        var notification = _problemNotifications.FirstOrDefault(n => n.Id == id);
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

    public void Create(Notification notification)
    {
        _notifications.Add(notification);
        _stakeholdersContext.SaveChanges();
    }

    public IEnumerable<Notification> GetAll()
    {
        return _notifications
            .OrderByDescending(n => n.CreatedAt)
            .ToList();
    }
    public Notification NotificationById(long id)
    {
        var notification = _notifications.FirstOrDefault(n => n.Id == id);
        return notification == null ? throw new ArgumentException("Invalid notification ID.") : notification;
    }

    public Notification SaveNotification(Notification notification)
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

