using Explorer.Stakeholders.API.Dtos;
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

    }
}
