using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.UseCases;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Encounter
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/encounter")]
    public class EncounterController : BaseApiController
    {
        private readonly IEncounterService _encounterService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EncounterController(IEncounterService encounterService, IWebHostEnvironment webHostEnvironment)
        {
            _encounterService = encounterService;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost]
        public ActionResult<EncounterDto> Create([FromBody] EncounterDto encounterDto)
        {
            if (encounterDto is HiddenLocationEncounterDto hiddenLocationEncounterDto) {
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
    }
}
