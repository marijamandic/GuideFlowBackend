using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

public interface INotificationRepository
{
    void Create(ProblemNotification notification);
    PagedResult<ProblemNotification> GetByUserId(long userId);
}
