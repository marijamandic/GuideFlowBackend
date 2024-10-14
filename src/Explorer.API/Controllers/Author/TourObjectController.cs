using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/administration/tourObject")]
    public class TourObjectController : BaseApiController
    {
        private readonly ITourObjectService _tourObjectService;

        public TourObjectController(ITourObjectService tourObjectService)
        {
            _tourObjectService = tourObjectService;
        }

        [HttpPost]
        public ActionResult<TourObjectDto> Create([FromBody] TourObjectDto tourObject)
        {
            var result = _tourObjectService.Create(tourObject);
            return CreateResponse(result);
        }
    }
}
