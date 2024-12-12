using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace Explorer.API.Controllers.Tourist
{
    //[Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/tourSpecifications")]
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
            var result = _tourSpecificationService.Create(tour);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<TourSpecificationDto> Update([FromBody] TourSpecificationDto tour)
        {
            var result = _tourSpecificationService.Update(tour);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _tourSpecificationService.Delete(id);
            return CreateResponse(result);
        }


        [HttpGet("{userId:long}")]
        public ActionResult<TourSpecificationDto> GetByUserId(long userId)
        {
            var result = _tourSpecificationService.GetByUserId(userId);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return NotFound(result.Errors);
        }
    }
}
