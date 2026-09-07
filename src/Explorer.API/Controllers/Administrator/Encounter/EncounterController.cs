using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Tours.API.Dtos.Execution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Encounter
{
   // [Authorize(Policy ="administratorPolicy")]
    [Route("api/admin/encounter")]
    public class EncounterController : BaseApiController
    {
        private readonly IEncounterService _encounterService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EncounterController(IEncounterService encounterService, IWebHostEnvironment webHostEnvironment)
        {
            _encounterService = encounterService;
            _webHostEnvironment = webHostEnvironment;
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
            if (encounterDto is HiddenLocationEncounterDto hiddenLocationEncounterDto)
            {
                if (!string.IsNullOrEmpty(hiddenLocationEncounterDto.ImageBase64))
                {
                    var imageData = Convert.FromBase64String(hiddenLocationEncounterDto.ImageBase64.Split(',')[1]);
                    var fileName = Guid.NewGuid() + ".png";
                    var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "encounters");

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    var filePath = Path.Combine(folderPath, fileName);
                    System.IO.File.WriteAllBytes(filePath, imageData);
                    hiddenLocationEncounterDto.ImageUrl = $"images/encounters/{fileName}";
                    var resultHidden = _encounterService.Create(hiddenLocationEncounterDto);
                    return CreateResponse(resultHidden);
                }
            }
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
