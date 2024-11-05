using Explorer.API.Controllers;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Author;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Tour
{
    //[Authorize(Policy = "authorPolicy")]
    [Route("api/author/tours")]
    public class TourController : BaseApiController
    {
        private readonly ITourService _tourService;

        public TourController(ITourService tourService)
        {
            _tourService = tourService;
        }

        [HttpGet]
        public ActionResult<PagedResult<TourDto>> GetPaged([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<TourDto> GetTour(int id)
        {
            var result = _tourService.Get(id);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<TourDto> Create([FromBody] TourDto tour)
        {
            var result = _tourService.Create(tour);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<TourDto> Update(int id, [FromBody] TourDto tour)
        {
            var result = _tourService.Update(tour);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _tourService.Delete(id);
            return CreateResponse(result);
        }

        [HttpPut("publish/{id:int}")]
        public ActionResult<TourDto> Publish(int id, [FromBody] TourDto tour)
        {
            var result = _tourService.Publish(tour);
            return CreateResponse(result);
        }
    }
}
