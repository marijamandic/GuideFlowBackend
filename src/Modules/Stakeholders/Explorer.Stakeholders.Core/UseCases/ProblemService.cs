using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
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
    private IMapper _mapper;

    public ProblemService(
        IMapper mapper,
        IProblemRepository problemRepository,
        IInternalProblemService internalProblemService) : base(mapper)
    {
        _problemRepository = problemRepository;
        _internalProblemService = internalProblemService;
        _mapper = mapper;
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

    public Result<PagedResult<MessageDto>> CreateMessage(int userId, CreateMessageInputDto messageInput)
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
        problem = _problemRepository.Save(problem);
        var messages = new List<Message>(problem.Messages);
        var messageDtos = messages.Select(p => _mapper.Map<MessageDto>(p)).ToList();
        return new PagedResult<MessageDto>(messageDtos, messageDtos.Count);
    }

    public Result<PagedResult<ProblemDto>> GetByTouristId(int touristId)
    {
        var problems = _problemRepository.GetByTouristId(touristId);
        return MapToDto(problems);
    }
}
