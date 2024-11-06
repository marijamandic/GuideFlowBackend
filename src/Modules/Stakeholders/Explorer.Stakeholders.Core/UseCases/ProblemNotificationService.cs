using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases;

public class ProblemNotificationService : BaseService<ProblemNotificationDto, ProblemNotification> , IProblemNotificationService
{
    private readonly IProblemNotificationRepository _notificationRepository;

    public ProblemNotificationService(IMapper mapper, IProblemNotificationRepository notificationRepository) : base(mapper)
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
}
