using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public.Administration;
using Explorer.Stakeholders.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/manageclub/club")]
    public class ClubController : BaseApiController
    {
        private readonly IClubService _clubService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ClubController(IClubService clubService, IWebHostEnvironment webHostEnvironment)
        {
            _clubService = clubService;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public ActionResult<PagedResult<ClubDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _clubService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }
        [HttpPost]
        public ActionResult<ClubDto> Create([FromBody] ClubDto clubDto)
        {
            if (!string.IsNullOrEmpty(clubDto.ImageBase64))
            {
                var imageData = Convert.FromBase64String(clubDto.ImageBase64.Split(',')[1]);
                var fileName = Guid.NewGuid() + ".png";
                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "clubs");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var filePath = Path.Combine(folderPath, fileName);
                System.IO.File.WriteAllBytes(filePath, imageData);
                clubDto.ImageUrl = $"images/clubs/{fileName}";
            }
            var result = _clubService.Create(clubDto);
            return CreateResponse(result);
        }
        [HttpPut("{id:int}")]
        public ActionResult<ClubDto> Update([FromBody] ClubDto clubDto)
        {
            if (!string.IsNullOrEmpty(clubDto.ImageBase64))
            {
                var imageData = Convert.FromBase64String(clubDto.ImageBase64.Split(',')[1]);
                var fileName = Guid.NewGuid() + ".png";
                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "clubs");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var filePath = Path.Combine(folderPath, fileName);
                System.IO.File.WriteAllBytes(filePath, imageData);
                clubDto.ImageUrl = $"images/clubs/{fileName}";
            }
            var result = _clubService.Update(clubDto);
            return CreateResponse(result);
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _clubService.Delete(id);
            return CreateResponse(result);
        }

    }
}
