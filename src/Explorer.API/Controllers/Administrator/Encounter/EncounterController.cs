using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Tours.API.Dtos.Execution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Encounter
{
   // [Authorize(Policy ="administratorPolicy")]
    [Route("api/admin/encounter")]
    public class EncounterController : BaseApiController
    {
        private readonly IEncounterService _encounterService;
        public EncounterController(IEncounterService encounterService)
        {
            _encounterService = encounterService;
        }

        [HttpGet]
        public ActionResult<PagedResult<EncounterDto>> GetAll([FromQuery] int page , [FromQuery] int pageSize) {
            var result = _encounterService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }
        [HttpGet("{id:long}")]
        public ActionResult<EncounterDto> GetById([FromRoute] long id) {
            var result = _encounterService.Get(id);
            return CreateResponse(result);
        }
        [HttpPost]
        public ActionResult<EncounterDto> Create([FromBody] EncounterDto encounterDto)
        {
            var result = _encounterService.Create(encounterDto);
            return CreateResponse(result);
        }
        [HttpPut]
        public ActionResult<EncounterDto> Update([FromBody] EncounterDto encounterDto)
        {
            var result = _encounterService.Update(encounterDto);
            return CreateResponse(result);
        }
        [HttpGet("search")]
        public ActionResult<PagedResult<EncounterDto>> SearchAndFilter([FromQuery] SearchAndFilterParamsDto search)
        {
            var result = _encounterService.SearchAndFilter(search.Name, search.Type,search.UserLatitude,search.UserLongitude);
            return CreateResponse(result);
        }
    }
}
