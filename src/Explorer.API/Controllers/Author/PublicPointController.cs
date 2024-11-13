using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    // [Authorize(Policy = "authorPolicy")]
    [Route("api/administration/publicpoint")]
    public class PublicPointController : BaseApiController
    {
        private readonly IPublicPointService _publicPointService;

        public PublicPointController(IPublicPointService publicPointService)
        {
            _publicPointService = publicPointService;
        }

        [HttpPost]
        public ActionResult<PublicPointDto> Create([FromBody] PublicPointDto publicPoint)
        {
            var result = _publicPointService.Create(publicPoint);
            return CreateResponse(result);
        }

        [HttpPut("{id}")]
        public ActionResult<PublicPointDto> Update([FromRoute] double id, [FromBody] PublicPointDto publicPoint)
        {
            if (id != publicPoint.Id) 
            {
                return BadRequest("ID mismatch.");
            }
            var result = _publicPointService.Update(publicPoint);
            return CreateResponse(result);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var result = _publicPointService.Delete(id);
            return CreateResponse(result);
        }

        [HttpGet("{id}")]
        public ActionResult<PublicPointDto> Get(int id)
        {
            var result = _publicPointService.Get(id);

            if (!result.IsSuccess)
            {
                return NotFound(); // Return a 404 Not Found response
            }

            return CreateResponse(result);
        }


        [HttpGet]
        public ActionResult<IEnumerable<PublicPointDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _publicPointService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("pending")]
        public ActionResult<IEnumerable<PublicPointDto>> GetPendingPublicPoints()
        {
            var result = _publicPointService.GetPendingPublicPoints();
            return CreateResponse(result);
        }
    }
}
