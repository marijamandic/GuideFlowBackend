using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Encounter
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/encounterExecution")]
    public class EncounterExecutionController : BaseApiController
    {

        private readonly IEncounterExecutionService _encounterExecutionService;
        public EncounterExecutionController(IEncounterExecutionService encounterExecutionService)
        {
            _encounterExecutionService = encounterExecutionService;
        }

        [HttpGet("{id:long}")]
        public ActionResult<EncounterExecutionDto> GetById([FromRoute] long id)
        {
            var result = _encounterExecutionService.Get(id);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<EncounterExecutionDto> Create([FromBody] EncounterExecutionDto encounterExecutionDto)
        {
            var result = _encounterExecutionService.Create(encounterExecutionDto);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<EncounterExecutionDto> Update([FromBody] EncounterExecutionDto encounterExecutionDto)
        {
            var result = _encounterExecutionService.Update(encounterExecutionDto);
            return CreateResponse(result);
        }

        [HttpGet("getByUser/{userId:long}")]
        public ActionResult<EncounterExecutionDto> GetByUserId([FromRoute] long userId)
        {
            var result = _encounterExecutionService.GetByUser(userId);
            return CreateResponse(result);
        }

        [HttpGet("findExecution/{userId:long}/{encounterId:long}")]
        public ActionResult<EncounterExecutionDto> FindExecution([FromRoute] long userId, [FromRoute] long encounterId)
        {
            var result = _encounterExecutionService.FindExecution(userId, encounterId);

            if (result.Value == null)
            {
                return Ok(null); // Umesto greške, vraća se null
            }

            return CreateResponse(result);
        }

    }
}
