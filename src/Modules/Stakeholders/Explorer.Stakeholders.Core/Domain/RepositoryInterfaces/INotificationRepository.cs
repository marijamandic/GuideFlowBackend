using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

public interface INotificationRepository
{
    ProblemNotification Save(ProblemNotification notification);
    void Create(ProblemNotification notification);
    PagedResult<ProblemNotification> GetByUserId(long userId);
    ProblemNotification GetById(long id);
    void Create(Notification notification); // Kreiranje Notification
    IEnumerable<Notification> GetAll();    // Dohvat svih Notification
    Notification NotificationById(long id);
    Notification SaveNotification(Notification notification);
    void Delete(Notification notification);
}
