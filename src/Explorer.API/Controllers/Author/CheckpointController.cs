using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/administration/checkpoint")]
    public class CheckpointController : BaseApiController
    {
        private readonly ICheckpointService _checkpointService;

        public CheckpointController(ICheckpointService checkpointService)
        {
            _checkpointService = checkpointService;
        }

        [HttpPost]
        public ActionResult<CheckpointDto> Create([FromBody] CheckpointDto checkpoint)
        {
            var result = _checkpointService.Create(checkpoint);
            return CreateResponse(result);
        }

        [HttpPut("{id}")]
        public ActionResult<CheckpointDto> Update([FromBody] CheckpointDto checkpoint)
        {
            var result = _checkpointService.Update(checkpoint);
            return CreateResponse(result);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var result = _checkpointService.Delete(id);
            return CreateResponse(result);
        }

        [HttpGet("{id}")]
        public ActionResult<CheckpointDto> Get(int id)
        {
            var result = _checkpointService.Get(id);
            return CreateResponse(result);
        }

        [HttpGet]
        public ActionResult<IEnumerable<CheckpointDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _checkpointService.GetPaged(page,  pageSize);
            return CreateResponse(result);
        }
    }
}
