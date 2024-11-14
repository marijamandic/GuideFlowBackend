using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

public interface INotificationRepository
{
    ProblemNotification Save(ProblemNotification notification);
    void Create(ProblemNotification notification);
    PagedResult<ProblemNotification> GetByUserId(long userId);
    ProblemNotification GetById(long id);
}
