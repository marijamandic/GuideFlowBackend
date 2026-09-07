using Explorer.API.Controllers.Administrator.Administration;
using Explorer.Stakeholders.API.Dtos.Club;
using Explorer.Stakeholders.API.Public.Club;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Explorer.Stakeholders.API.Public;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Route("api/administration/clubpost")]
    [ApiController]
    public class ClubPostController : ControllerBase
    {
        private readonly IClubPostService _clubPostService;

        public ClubPostController(IClubPostService clubPostService)
        {
            _clubPostService = clubPostService;
        }

        // GET: api/administration/clubpost
        [HttpGet]
        public ActionResult<List<ClubPostDto>> GetAll()
        {
            var result = _clubPostService.GetAll();
            return result.IsSuccess ? Ok(result.Value) : StatusCode(500, result.Errors);
        }

        // GET: api/administration/clubpost/{clubId}
        [HttpGet("{clubId}")]
        public ActionResult<List<ClubPostDto>> GetByClubId(long clubId)
        {
            var result = _clubPostService.GetByClubId(clubId);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errors);
        }

        // POST: api/administration/clubpost
        [HttpPost]
        public ActionResult<ClubPostDto> Create(ClubPostDto clubPostDto)
        {
            var result = _clubPostService.Create(clubPostDto);  // Assuming Add method is defined in IClubPostService
            return result.IsSuccess ? CreatedAtAction(nameof(GetByClubId), new { clubId = result.Value.ClubId }, result.Value) : BadRequest(result.Errors);
        }

        // PUT: api/administration/clubpost/{id}
        [HttpPut("{id}")]
        public ActionResult Update(long id, ClubPostDto clubPostDto)
        {
            clubPostDto.Id = id;  // Ensure the DTO has the correct ID
            var result = _clubPostService.Update(clubPostDto);  // Assuming Update method is defined in IClubPostService
            return result.IsSuccess ? NoContent() : NotFound(result.Errors);
        }

        // DELETE: api/administration/clubpost/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var result = _clubPostService.Delete(id);  // Assuming Delete method is defined in IClubPostService
            return result.IsSuccess ? NoContent() : NotFound(result.Errors);
        }
    }
}
