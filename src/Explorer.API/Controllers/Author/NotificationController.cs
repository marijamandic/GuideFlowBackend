using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author;

[Authorize(Policy = "authorPolicy")]
[Route("api/notifications/author/problem")]
public class NotificationController : BaseApiController
{
    private readonly INotificationService _notificationService;
    private readonly NotificationMoneyExchangeService _moneyExchangeService;
    public NotificationController(INotificationService notificationService, NotificationMoneyExchangeService moneyExchangeService)
    {
        _notificationService = notificationService;
        _moneyExchangeService = moneyExchangeService;
    }

    [HttpGet]
    public ActionResult<PagedResult<ProblemNotificationDto>> GetByUserId()
    {
        if (int.TryParse(User.FindFirst("id")!.Value, out int userId))
        {
            var result = _notificationService.GetByUserId(userId);
            return CreateResponse(result);
        }
        else return BadRequest("Invalid user");
    }

    [HttpPatch]
    public ActionResult<ProblemNotificationDto> PatchIsOpened([FromQuery] int id, [FromQuery] bool isOpened)
    {
        int userId = int.Parse(User.FindFirst("id")!.Value);
        var result = _notificationService.PatchIsOpened(id, userId, isOpened);
        return CreateResponse(result);
    }

    [HttpGet("by-user/{userId}")]
    public ActionResult<IEnumerable<NotificationDto>> NotificationsByUser(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest("Invalid user ID.");
        }

        var result = _moneyExchangeService.GetNotificationsByUserId(userId);
        return CreateResponse(result);
    }
}
