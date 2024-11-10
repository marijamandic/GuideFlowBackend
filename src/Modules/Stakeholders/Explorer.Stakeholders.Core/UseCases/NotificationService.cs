using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases;

public class NotificationService : BaseService<ProblemNotificationDto, ProblemNotification> , INotificationService
{
    private readonly INotificationRepository _notificationRepository;

    public NotificationService(IMapper mapper, INotificationRepository notificationRepository) : base(mapper)
    {
        _notificationRepository = notificationRepository;
    }

    public Result Create(CreateProblemNotificationInputDto notificationInput)
    {
        var notification = new ProblemNotificationDto
        {
            UserId = notificationInput.UserId,
            Sender = notificationInput.Sender,
            Message = notificationInput.Message,
            IsOpened = false,
            ProblemId = notificationInput.ProblemId
        };

        _notificationRepository.Create(MapToDomain(notification));
        return Result.Ok();
    }

    public Result<PagedResult<ProblemNotificationDto>> GetByUserId(int userId)
    {
        var result = _notificationRepository.GetByUserId(userId);
        return MapToDto(result);
    }
}
