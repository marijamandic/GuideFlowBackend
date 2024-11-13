using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/tourist/AppRating")]
    public class RatingsController : BaseApiController
    {
        private readonly IRatingAppService _ratingAppService;

        public RatingsController(IRatingAppService ratingAppService)
        {
            _ratingAppService = ratingAppService;
        }

        [HttpGet("all")]
        public ActionResult<IEnumerable<RatingAppDto>> GetAllRatings([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _ratingAppService.GetPaged(page, pageSize);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }
    }
}
