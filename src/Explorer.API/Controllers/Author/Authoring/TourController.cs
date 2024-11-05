using Explorer.API.Controllers;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Author;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Authoring.Tour
{
    //[Authorize(Policy = "authorPolicy")]
    [Route("api/authoring/tour")]
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
        public ActionResult<TourDto> Update([FromBody] TourDto tour)
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

        [HttpPut("addingCheckpoint/{id:int}")]
        public ActionResult<TourDto> AddCheckpoint(int id,[FromBody] CheckpointAdditionDto checkpointAddition)
        {
            var result = _tourService.AddCheckpoint(id, checkpointAddition.Checkpoint,checkpointAddition.UpdatedLength);
            return CreateResponse(result);
        }

        [HttpPut("addingTransportDuration/{id:int}")]
        public ActionResult<TourDto> AddTransportDurations(int id, [FromBody] List<TransportDurationDto> transportDurations)
        {
            var result = _tourService.AddTransportDurations(id, transportDurations);
            return CreateResponse(result);
        }

        [HttpPut("archive/{id:int}")]
        public ActionResult<TourDto> Archive(int id)
        {
            var result = _tourService.Archive(id);
            return CreateResponse(result);
        }


        [HttpPut("publish/{id:int}")]
        public ActionResult<TourDto> Publish(int id)
        {
            var result = _tourService.Publish(id);
            return CreateResponse(result);
        }
    }
}
