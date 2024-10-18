using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    //[Authorize(Policy = "tourist_Policy")]
    [Route("api/tourist/tourspecifications")]
    public class TourSpecificationController : BaseApiController
    {
        private readonly ITourSpecificationService _tourSpecificationService;

        public TourSpecificationController(ITourSpecificationService tourSpecificationService)
        {
            _tourSpecificationService = tourSpecificationService;
        }

        [HttpGet]
        public ActionResult<PagedResult<TourSpecificationDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourSpecificationService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<TourSpecificationDto> Create([FromBody] TourSpecificationDto tour)
        {
            var result = _tourSpecificationService.CreateTourSpecifications(tour);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<TourSpecificationDto> Update(long id, [FromBody] TourSpecificationDto tour)
        {
            if (id != tour.UserId)
            {
                return BadRequest("ID u putanji ne odgovara ID-u korisnika.");
            }

            var result = _tourSpecificationService.UpdateTourSpecifications(tour);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _tourSpecificationService.DeleteTourSpecifications(id);
            return CreateResponse(result);
        }


        [HttpGet("{userId:long}")]
        public ActionResult<TourSpecificationDto> GetByUserId(long userId)
        {
            var result = _tourSpecificationService.GetTourSpecificationsByUserId(userId);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return NotFound(result.Errors);
        }



    }
}
