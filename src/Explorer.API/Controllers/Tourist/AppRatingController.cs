using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/AppRating")]
    public class AppRatingController : BaseApiController
    {
        private readonly IRatingAppService _ratingAppService;

        public AppRatingController(IRatingAppService ratingAppService)
        {
            _ratingAppService = ratingAppService;
        }

        [HttpPost("rating")]
        public ActionResult<RatingAppDto> NewAppRating([FromBody] RatingAppDto ratingAppDto)
        {
            //ratingAppDto = (DateTime)ratingAppDto.RatingTime;
            var result = _ratingAppService.Create(ratingAppDto);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }

        //[HttpGet("all")]
        //public ActionResult<IEnumerable<RatingAppDto>> GetAllRatings([FromQuery] int page, [FromQuery] int pageSize)
        //{
        //    var result = _ratingAppService.GetPaged(page, pageSize);

        //    if (result.IsFailed)
        //    {
        //        return BadRequest(result.Errors);
        //    }

        //    return Ok(result.Value);
        //}
    }
}