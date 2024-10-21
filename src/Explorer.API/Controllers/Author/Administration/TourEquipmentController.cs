using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Administration

{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/administration/tourEquipment")]
    public class TourEquipmentController : BaseApiController
    {

        private readonly ITourEquipmentService _tourEquipmentService;

        public TourEquipmentController(ITourEquipmentService tourEquipmentService)
        {
            _tourEquipmentService = tourEquipmentService;
        }

        [HttpGet]
        public ActionResult<PagedResult<TourEquipmentDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourEquipmentService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }


        [HttpPost]
        public ActionResult<TourEquipmentDto> Create([FromBody] TourEquipmentDto tourEquipment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _tourEquipmentService.Create(tourEquipment);
            if (result == null)
            {
                return NotFound(); // Ili neka druga odgovarajuća greška
            }
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<TourEquipmentDto> Update([FromBody] TourEquipmentDto tourEquipment)
        {
            var result = _tourEquipmentService.Update(tourEquipment);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _tourEquipmentService.Delete(id);
            return CreateResponse(result);
        }
        [HttpGet("{id:int}")]
        public List<EquipmentDto> GetEquipmentByTour([FromRoute] int id)
        {
            // Poziva servis koji vraća listu EquipmentDto
            var result = _tourEquipmentService.GetEquipmentByTour(id);

            // Proverava da li su rezultati prazni
            if (result == null || !result.Any())
            {
                return null;
                //return NotFound("No equipment found for the given tour ID.");
            }
            return result;
            // Kreira FluentResults.Result sa listom EquipmentDto
            //var fluentResult = FluentResults.Result.Ok(result);

            // Vraća rezultat kroz metodu za kreiranje HTTP odgovora
            //return CreateResponse(fluentResult);
        }



    }
}
