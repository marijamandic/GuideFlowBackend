using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FluentResults;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/equipmentManagement")]
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
            var result = _equipmentService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<EquipmentManagementDto> Create([FromBody] EquipmentManagementDto entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _equipmentService.Create(entity);
            if (result == null)
            {
                return NotFound();
            }

            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<EquipmentManagementDto> Update([FromBody] EquipmentManagementDto tourEquipment)
        {
            var result = _equipmentService.Update(tourEquipment);
            return CreateResponse(result);
        }


        [HttpDelete("{equipmentId:int}")]
        public ActionResult DeleteEquipment(int equipmentId)
        {
            var result = _equipmentService.DeleteEquipmentById(equipmentId);
            return CreateResponse(result);
        }

        [HttpGet("{userId:long}")]
        public ActionResult<PagedResult<EquipmentManagementDto>> GetByTourist(int id)
        {
            var result = _equipmentService.GetEquipmentByUser(id);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return NotFound(result.Errors);
        }
    }
}
