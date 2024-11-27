using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Dtos.Problems;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.Problems;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Internal;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases;
public class ProblemService : BaseService<ProblemDto, Problem>, IProblemService
{
    private readonly IProblemRepository _problemRepository;

    private readonly IInternalProblemService _internalProblemService;
    private readonly INotificationService _notificationService;

    public ProblemService(
        IMapper mapper,
        IProblemRepository problemRepository,
        IInternalProblemService internalProblemService,
        INotificationService notificationService) : base(mapper)
    {
        _problemRepository = problemRepository;
        _internalProblemService = internalProblemService;
        _notificationService = notificationService;
    }

    public Result<ProblemDto> Create(CreateProblemInputDto problemInput)
    {
        var problem = new ProblemDto
        {
            UserId = problemInput.UserId,
            TourId = problemInput.TourId,
            Details = new DetailsDto
            {
                Category = problemInput.Category,
                Priority = problemInput.Priority,
                Description = problemInput.Description
            },
            Resolution = new ResolutionDto
            {
                ReportedAt = DateTime.Now,
                IsResolved = false,
                Deadline = DateTime.Now.AddDays(5)
            }
        };

        var result = _problemRepository.Create(MapToDomain(problem));
        return MapToDto(result);
    }

    public Result<PagedResult<ProblemDto>> GetAll()
    {
        var problems = _problemRepository.GetAll();
        return MapToDto(problems);
    }

    public Result<PagedResult<ProblemDto>> GetByAuthorId(int authorId)
    {
        var tourIds = _internalProblemService.GetTourIdsByAuthorId(authorId);
        var problems = _problemRepository.GetByTourIds(tourIds.Value);
        return MapToDto(problems);
    }

    private Problem CreateMessage(CreateMessageInputDto messageInput, int userId)
    {
        var message = new MessageDto
        {
            ProblemId = messageInput.ProblemId,
            UserId = userId,
            Content = messageInput.Content,
            PostedAt = DateTime.UtcNow
        };

        var problem = _problemRepository.GetById(messageInput.ProblemId);
        problem.AddMessage(_mapper.Map<Message>(message));
        return _problemRepository.Save(problem);
    }

    private void SendNewMsgNotification(CreateMessageInputDto messageInput, UserDto jwtUser, Problem problem)
    {
        int userId = jwtUser.Role == UserRole.Author ?
            (int)problem.UserId :
            _internalProblemService.GetAuthorIdByTourId(problem.TourId).Value;

        var notificationInput = new CreateProblemNotificationInputDto
        {
            UserId = userId,
            Sender = jwtUser.Username,
            Message = messageInput.Content,
            ProblemId = messageInput.ProblemId,
            Pnt = ProblemNotificationType.NewMessage
        };

        _notificationService.Create(notificationInput);
    }

    public Result<PagedResult<MessageDto>> CreateMessage(CreateMessageInputDto messageInput, UserDto jwtUser)
    {
        var problem = CreateMessage(messageInput, jwtUser.Id);
        SendNewMsgNotification(messageInput, jwtUser, problem);

        var messages = problem.Messages.Select(p => _mapper.Map<MessageDto>(p)).ToList();
        return new PagedResult<MessageDto>(messages, messages.Count);
    }

    public Result<PagedResult<ProblemDto>> GetByTouristId(int touristId)
    {
        var problems = _problemRepository.GetByTouristId(touristId);
        return MapToDto(problems);
    }

    public Result<PagedResult<ProblemDto>> GetUserProblems(int userId)
    {
        var problems = _problemRepository.GetUserProblems(userId);
        return MapToDto(problems);
    }

    public Result<ProblemDto> Update(ProbStatusChangeDto status, int id)
    {
        var problem = _problemRepository.GetById(id);
        problem.ChangeStatus(status.TouristMessage, status.IsSolved);
        var result = _problemRepository.Update(problem);
        return MapToDto(result);
    }
    public Result<ProblemDto> UpdateDeadline(int id, DateTime deadline, UserDto jwtUser)
    {
        var problem = _problemRepository.GetById(id);
        problem.ChangeDeadline(deadline);
        var result = _problemRepository.Update(problem);

        // send notification
        var userId = _internalProblemService.GetAuthorIdByTourId(problem.TourId);

        var notification = new CreateProblemNotificationInputDto
        {
            UserId = userId.Value,
            Sender = jwtUser.Username,
            Message = $"New problem resoltion deadline set to {deadline.ToShortDateString()}",
            ProblemId = id,
            Pnt = ProblemNotificationType.NewDeadline
        };

        _notificationService.Create(notification);

        return MapToDto(result);
    }
}
