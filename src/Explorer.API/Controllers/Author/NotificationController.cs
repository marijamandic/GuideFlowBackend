using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author;

[Authorize(Policy = "authorPolicy")]
[Route("api/notifications/author/problem")]
public class NotificationController : BaseApiController
{
    private readonly IProblemNotificationService _problemNotificationService;

    public NotificationController(IProblemNotificationService problemNotificationService)
    {
        _problemNotificationService = problemNotificationService;
    }

    [HttpGet]
    public ActionResult<PagedResult<ProblemNotificationDto>> GetByUserId()
    {
        if (int.TryParse(User.FindFirst("id")!.Value, out int userId))
        {
            var result = _problemNotificationService.GetByUserId(userId);
            return CreateResponse(result);
        }
        else return BadRequest("Invalid user");
    }
}
