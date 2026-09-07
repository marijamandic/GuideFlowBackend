using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace Explorer.API.Controllers.Author
{
    //[Authorize(Policy = "authorPolicy")]
    [Route("api/administration/tourObject")]
    public class TourObjectController : BaseApiController
    {
        private readonly ITourObjectService _tourObjectService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TourObjectController(ITourObjectService tourObjectService, IWebHostEnvironment webHostEnvironment)
        {
            _tourObjectService = tourObjectService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public ActionResult<PagedResult<TourObjectDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourObjectService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<TourObjectDto> Create([FromBody] TourObjectDto tourObject)
        {
            if (!string.IsNullOrEmpty(tourObject.ImageBase64))
            {
                var imageData = Convert.FromBase64String(tourObject.ImageBase64.Split(',')[1]);
                var fileName = Guid.NewGuid() + ".png";
                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "blogs");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var filePath = Path.Combine(folderPath, fileName);
                System.IO.File.WriteAllBytes(filePath, imageData);
                tourObject.ImageUrl = $"images/blogs/{fileName}";
            }
            var result = _tourObjectService.Create(tourObject);
            return CreateResponse(result);
        }

        [HttpPut("{id}")]
        public ActionResult<TourObjectDto> Update(int id, [FromBody] TourObjectDto tourObject)
        {

            // Ako je poslat novi Base64 string slike, ažurirajte sliku
            if (!string.IsNullOrEmpty(tourObject.ImageBase64))
            {
                var imageData = Convert.FromBase64String(tourObject.ImageBase64.Split(',')[1]);
                var fileName = Guid.NewGuid() + ".png";
                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "blogs");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var filePath = Path.Combine(folderPath, fileName);
                System.IO.File.WriteAllBytes(filePath, imageData);
                tourObject.ImageUrl = $"images/blogs/{fileName}";
            }

            // Ažuriraj tourObject koristeći servis
            var result = _tourObjectService.Update(tourObject);

            // Kreiraj odgovor
            return CreateResponse(result);
        }

    }
}
