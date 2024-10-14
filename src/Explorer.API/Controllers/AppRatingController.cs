using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers
{
    [Authorize(Policy = "touristPolicy")]
    [Microsoft.AspNetCore.Mvc.Route("api/tourist/AppRating")] // Koristite potpuni naziv
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
            var result = _ratingAppService.Create(ratingAppDto);
            return CreateResponse(result);
        }
    }
}
