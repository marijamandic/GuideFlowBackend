using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist;

[Authorize(Policy = "touristPolicy")]
[Route("api/notifications/tourist/problem")]
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

    [HttpPatch("{id}")]
    public ActionResult UpdateNotification(int id, [FromBody] NotificationDto updatedNotification)
    {
        var result = _moneyExchangeService.UpdateNotification(id, updatedNotification);
        return CreateResponse(result);
    }

}
