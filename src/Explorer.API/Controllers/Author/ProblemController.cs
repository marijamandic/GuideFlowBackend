using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Dtos.Problems;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Explorer.API.Controllers.Author;

[Authorize(Policy = "authorPolicy")]
[Route("api/author/problems")]
public class ProblemController : BaseApiController
{
    private readonly IProblemService _problemService;

    public ProblemController(IProblemService problemService)
    {
        _problemService = problemService;
    }

    [HttpGet]
    public ActionResult<PagedResult<ProblemDto>> GetByAuthorId()
    {
        if (int.TryParse(User.FindFirst("id")?.Value, out int authorId))
        {
            var result = _problemService.GetByAuthorId(authorId);
            return CreateResponse(result);
        }
        else
        {
            return BadRequest("Invalid input");
        }
    }

    [HttpPost("messages")]
    public ActionResult<PagedResult<MessageDto>> CreateMessage([FromBody] CreateMessageInputDto messageInput)
    {
        bool isBadRequest = !int.TryParse(User.FindFirst("id")?.Value, out int authorId) ||
            string.IsNullOrWhiteSpace(messageInput.Content);

        if (isBadRequest) return BadRequest("Invalid input");

        var jwtUser = new UserDto
        {
            Id = authorId,
            Role = (UserRole)Enum.Parse(typeof(UserRole), User.FindFirst(ClaimTypes.Role)!.Value),
            Username = User.FindFirst("username")!.Value
        };
        var result = _problemService.CreateMessage(messageInput, jwtUser);
        return CreateResponse(result);
    }
}
