using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FluentResults;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/equipmentManagement/equipment")]
    public class EquipmentManagementController : BaseApiController
    {
        private readonly IEquipmentManagementService _equipmentService;

        public EquipmentManagementController(IEquipmentManagementService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpGet]
        public ActionResult<PagedResult<EquipmentManagementDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _equipmentService.GetEquipmentList();
            return CreateResponse(Result.Ok(result));
        }

        [HttpPost]
        public ActionResult<EquipmentDto> Create([FromBody] EquipmentManagementDto equipment)
        {
            var result = _equipmentService.AddEquipment(equipment);
            return CreateResponse(Result.Ok(result));
        }

        //[HttpPut("{id:int}")]
        //public ActionResult<EquipmentDto> Update([FromBody] EquipmentDto equipment)
        //{
        //    var result = _equipmentService.Update(equipment);
        //    return CreateResponse(result);
        //}

        //[HttpDelete("{id:int}")]
        //public ActionResult Delete(int id)
        //{
        //    var result = _equipmentService.RemoveEquipment(id);
        //    return CreateResponse(Result.Ok(result));
        //}
    }
}
