using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    // [Authorize(Policy = "authorPolicy")]
    [Route("api/administration/publicpointnotification")]
    public class PublicPointNotificationController : BaseApiController
    {
        private readonly IPublicPointNotificationService _publicPointNotificationService;

        public PublicPointNotificationController(IPublicPointNotificationService publicPointNotificationService)
        {
            _publicPointNotificationService = publicPointNotificationService;
        }

        [HttpPost]
        public ActionResult<PublicPointNotificationDto> Create([FromBody] PublicPointNotificationDto notification)
        {
            var result = _publicPointNotificationService.Create(notification);
            return CreateResponse(result);
        }

        [HttpPut("{id}")]
        public ActionResult<PublicPointNotificationDto> Update([FromRoute] int id, [FromBody] PublicPointNotificationDto notification)
        {
            if (id != notification.Id)
            {
                return BadRequest("ID mismatch.");
            }

            var result = _publicPointNotificationService.Update(notification);
            return CreateResponse(result);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var result = _publicPointNotificationService.Delete(id);
            return CreateResponse(result);
        }

        [HttpGet]
        public ActionResult<IEnumerable<PublicPointNotificationDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _publicPointNotificationService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("unread/{authorId}")]
        public ActionResult<IEnumerable<PublicPointNotificationDto>> GetUnreadByAuthor(int authorId)
        {
            var result = _publicPointNotificationService.GetUnreadByAuthor(authorId);
            return CreateResponse(result);
        }

        [HttpGet("author/{authorId}")]
        public ActionResult<IEnumerable<PublicPointNotificationDto>> GetAllByAuthor(int authorId)
        {
            var result = _publicPointNotificationService.GetAllByAuthor(authorId);
            return CreateResponse(result);
        }
    }
}
