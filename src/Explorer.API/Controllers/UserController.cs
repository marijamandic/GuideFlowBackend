using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Dtos.Problems;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("username/{userId}")]
        public ActionResult<object> GetUsername(int userId)
        {
            var result = _userService.GetById(userId);
            if (result.IsFailed || result.Value == null)
            {
                return NotFound("User not found");
            }
            return Ok(new { username = result.Value.Username }); 
        }

        [HttpGet("all")]
        public ActionResult<IEnumerable<UserDto>> GetAllUsers()
        {
            var result = _userService.GetAllUsers();
            if (result.IsFailed)
            {
                return BadRequest("Failed to fetch users");
            }
            return Ok(result.Value);
        }
        [HttpGet("getTourist/{touristId}")]
        public ActionResult<TouristDto> GetTouristById(int touristId)
        {
            var result = _userService.GetTouristById(touristId);
            if (result.IsFailed)
            {
                return BadRequest("Failed to fetch users");
            }
            return Ok(result.Value);
        }

        [HttpGet("getUser/{userId}")]
        public ActionResult<TouristDto> GetUserById(int userId)
        {
            var result = _userService.GetById(userId);
            if (result.IsFailed)
            {
                return BadRequest("Failed to fetch users");
            }
            return Ok(result.Value);
        }

        [HttpPut("updateMoney/{touristId}")]
        public ActionResult<TouristDto> UpdateTouristMoney(int touristId, [FromBody] int amount)
        {
            var result = _userService.AddTouristMoney(touristId, amount);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors.FirstOrDefault()?.Message ?? "Failed to update tourist money.");
            }
            return Ok(result.Value);
        }

        [HttpGet("getAuthor/{authorId}")]
        public ActionResult<AuthorDto> GetAuthorById(int authorId)
        {
            var result = _userService.GetAuthorById(authorId);
            if (result.IsFailed)
            {
                return BadRequest("Failed to fetch users");
            }
            return Ok(result.Value);
        }

        [HttpPut("updateAuthorMoney/{authorId}")]
        public ActionResult<AuthorDto> UpdateAuthorMoney(int authorId, [FromBody] int amount)
        {
            var result = _userService.AddAuthorMoney(authorId, amount);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors.FirstOrDefault()?.Message ?? "Failed to update author money.");
            }
            return Ok(result.Value);
        }
    }
}
