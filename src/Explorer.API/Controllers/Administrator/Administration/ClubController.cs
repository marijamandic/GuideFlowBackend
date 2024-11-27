using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos.Club;
using Explorer.Stakeholders.API.Public.Club;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Stakeholders.Core.UseCases.Club;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace Explorer.API.Controllers.Tourist
{
    // [Authorize(Policy = "touristPolicy")]
    [Route("api/manageclub/club")]
    public class ClubController : BaseApiController
    {
        private readonly IClubService _clubService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IClubMemberService _clubMemberService;

        public ClubController(IClubService clubService, IWebHostEnvironment webHostEnvironment, IClubMemberService clubMemberService)
        {
            _clubService = clubService;
            _webHostEnvironment = webHostEnvironment;
            _clubMemberService = clubMemberService;
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

        [HttpGet("top-by-members")]
        public ActionResult<List<ClubDto>> GetTopClubsByMembers()
        {
            int topCount = 5;
            var result = _clubService.GetTopClubsByMembers(topCount);
            return result.IsSuccess ? Ok(result.Value) : StatusCode(500, result.Errors);
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

        [HttpGet("{id:int}")]
        public ActionResult<ClubDto> GetClubById(int id)
        {
            var result = _clubService.Get(id);
            return CreateResponse(result); 
        }

    }
}
