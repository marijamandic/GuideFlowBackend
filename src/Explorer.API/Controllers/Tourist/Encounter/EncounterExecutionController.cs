using Explorer.Encounters.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Encounter
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/encounter")]
    public class EncounterExecutionController : BaseApiController
    {

        private readonly IEncounterExecutionService _encounterExecutionService;
        public EncounterExecutionController(IEncounterExecutionService encounterExecutionService)
        {
            _encounterExecutionService = encounterExecutionService;
        }
    }
}
