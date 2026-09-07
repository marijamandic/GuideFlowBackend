using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration;

[Authorize(Policy = "administratorPolicy")]
[Route("api/notifications/administrator/problem")]
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

    [HttpPost("money-exchange")]
    public ActionResult CreateNotification([FromBody] NotificationDto notificationDto)
    {
        var result = _moneyExchangeService.CreateNotification(notificationDto);
        return CreateResponse(result);
    }

}
