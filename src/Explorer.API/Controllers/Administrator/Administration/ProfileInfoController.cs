using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Explorer.API.Controllers.ProfileInfo
{
    //[Authorize(Policy = "administratorPolicy")]
    [Route("api/administration/profileInfo")]
    public class ProfileInfoController : BaseApiController
    {
        private readonly IProfileInfoService _profileInfoService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProfileInfoController(IProfileInfoService profileInfoService, IWebHostEnvironment webHostEnvironment)
        {
            _profileInfoService = profileInfoService;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: api/administration/profileInfo?page=1&pageSize=10
        [HttpGet]
        public ActionResult<PagedResult<ProfileInfoDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _profileInfoService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        // POST: api/administration/profileInfo
        [HttpPost]
        public ActionResult<ProfileInfoDto> Create([FromBody] ProfileInfoDto profileInfo)
        {

            if (!string.IsNullOrEmpty(profileInfo.ImageBase64))
            {
                var imageData = Convert.FromBase64String(profileInfo.ImageBase64.Split(',')[1]);
                var fileName = Guid.NewGuid() + ".png"; // ili bilo koji format slike
                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "profileInfo");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var filePath = Path.Combine(folderPath, fileName);
                System.IO.File.WriteAllBytes(filePath, imageData);
                profileInfo.ImageUrl = $"images/profileInfo/{fileName}";
            }

            var result = _profileInfoService.Create(profileInfo);
            return CreateResponse(result);
        }

        // PUT: api/administration/profileInfo/{id}/{userId}
        [HttpPut("{id:long}/{userId:long}")]
        public ActionResult<ProfileInfoDto> Update(long id, long userId, [FromBody] ProfileInfoDto profileInfo)
        {
            // Provera da li se id i userId podudaraju sa onima u DTO
            if (id != profileInfo.Id || userId != profileInfo.UserId)
            {
                return BadRequest("ID ili UserId se ne podudaraju.");
            }

            if (!string.IsNullOrEmpty(profileInfo.ImageBase64))
            {
                var imageData = Convert.FromBase64String(profileInfo.ImageBase64.Split(',')[1]);
                var fileName = Guid.NewGuid() + ".png"; // ili bilo koji format slike
                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "profileInfo");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var filePath = Path.Combine(folderPath, fileName);
                System.IO.File.WriteAllBytes(filePath, imageData);
                profileInfo.ImageUrl = $"images/profileInfo/{fileName}";
            }

            var result = _profileInfoService.Update(profileInfo);
            return CreateResponse(result);
        }

        // DELETE: api/administration/profileInfo/{id}
        [HttpDelete("{id:long}")]
        public ActionResult Delete(int id)
        {
            var result = _profileInfoService.Delete(id);
            return CreateResponse(result);
        }

        [HttpGet("{userId:long}")]
        public ActionResult<ProfileInfoDto> GetByUserId(int userId)
        {
            var result = _profileInfoService.GetByUserId(userId);
            if (result.IsSuccess)
            {
                return Ok(result.Value); // Ako je uspešno, vraćamo profil
            }

            return NotFound(result.Errors); // Ako nije, vraćamo grešku
        }

    }
}

