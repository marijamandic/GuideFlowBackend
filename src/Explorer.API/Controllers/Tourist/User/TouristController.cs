using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.UseCases;
using FluentResults;
using System.Collections.Generic;
using Explorer.Stakeholders.API.Public;

namespace Explorer.API.Controllers
{
    [Route("api/tourists")]
    public class TouristController : BaseApiController
    {
        private readonly IUserService _userService;

        public TouristController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetAllTourists(int page, int pageSize)
        {
            var result = _userService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<UserDto> GetTouristById(int id)
        {
            var result = _userService.GetById(id);
            return CreateResponse(result);
        }

        /*[HttpPost]
        public ActionResult<UserDto> CreateTourist([FromBody] UserDto userDto)
        {
            var result = _userService.CreateTourist(userDto);
            return CreateResponse(result);
        }*/

        [HttpPut("{id:int}")]
        public ActionResult<UserDto> UpdateTourist(int id, [FromBody] UserDto userDto)
        {
            userDto.Id = id;
            var result = _userService.Update(userDto);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteTourist(int id)
        {
            var result = _userService.Delete(id);
            return CreateResponse(result);
        }
    }
}
