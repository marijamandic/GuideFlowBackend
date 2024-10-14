using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
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

        [HttpGet]
        public ActionResult<PagedResult<TourObjectDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourObjectService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<TourObjectDto> Create([FromBody] TourObjectDto tourObject)
        {
            var result = _tourObjectService.Create(tourObject);
            return CreateResponse(result);
        }
    }
}
