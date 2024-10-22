using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.ProfileInfo
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administration/profileInfo")]
    public class ProfileInfoController : BaseApiController
    {
        private readonly IProfileInfoService _profileInfoService;

        public ProfileInfoController(IProfileInfoService profileInfoService)
        {
            _profileInfoService = profileInfoService;
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
            var result = _profileInfoService.Create(profileInfo);
            return CreateResponse(result);
        }

        // PUT: api/administration/profileInfo/{id}
        [HttpPut("{id:long}")]
        public ActionResult<ProfileInfoDto> Update(long id, [FromBody] ProfileInfoDto profileInfo)
        {
            if (id != profileInfo.Id)
            {
                return BadRequest("ID mismatch");
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
    }
}

