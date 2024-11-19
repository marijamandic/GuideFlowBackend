using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    // [Authorize(Policy = "authorPolicy")]
    [Route("api/administration/publicpoint")]
    public class PublicPointController : BaseApiController
    {
        private readonly IPublicPointService _publicPointService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PublicPointController(IPublicPointService publicPointService, IWebHostEnvironment webHostEnvironment)
        {
            _publicPointService = publicPointService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public ActionResult<PublicPointDto> Create([FromBody] PublicPointDto publicPoint)
        {
            if (!string.IsNullOrEmpty(publicPoint.ImageBase64))
            {
                var imageData = Convert.FromBase64String(publicPoint.ImageBase64.Split(',')[1]);
                var fileName = Guid.NewGuid() + ".png";
                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "publicPoints");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var filePath = Path.Combine(folderPath, fileName);
                System.IO.File.WriteAllBytes(filePath, imageData);
                publicPoint.ImageUrl = $"images/publicPoints/{fileName}";
            }
            var result = _publicPointService.Create(publicPoint);
            return CreateResponse(result);
        }

        [HttpPut("{id}")]
        public ActionResult<PublicPointDto> Update([FromRoute] double id, [FromBody] PublicPointDto publicPoint)
        {
            if (id != publicPoint.Id) 
            {
                return BadRequest("ID mismatch.");
            }
            var result = _publicPointService.Update(publicPoint);
            return CreateResponse(result);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var result = _publicPointService.Delete(id);
            return CreateResponse(result);
        }

        [HttpGet("{id}")]
        public ActionResult<PublicPointDto> Get(int id)
        {
            var result = _publicPointService.Get(id);

            if (!result.IsSuccess)
            {
                return NotFound(); // Return a 404 Not Found response
            }

            return CreateResponse(result);
        }


        [HttpGet]
        public ActionResult<IEnumerable<PublicPointDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _publicPointService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("pending")]
        public ActionResult<IEnumerable<PublicPointDto>> GetPendingPublicPoints()
        {
            var result = _publicPointService.GetPendingPublicPoints();
            return CreateResponse(result);
        }
    }
}
