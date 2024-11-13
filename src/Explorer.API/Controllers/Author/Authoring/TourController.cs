using Explorer.API.Controllers;
using Explorer.Blog.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Author;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Authoring.Tour
{
    //[Authorize(Policy = "authorPolicy")]
    [Route("api/authoring/tour")]
    public class TourController : BaseApiController
    {
        private readonly ITourService _tourService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TourController(ITourService tourService, IWebHostEnvironment webHostEnvironment)
        {
            _tourService = tourService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public ActionResult<PagedResult<TourDto>> GetPaged([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<TourDto> GetTour(int id)
        {
            var result = _tourService.Get(id);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<TourDto> Create([FromBody] TourDto tour)
        {
            var result = _tourService.Create(tour);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<TourDto> Update([FromBody] TourDto tour)
        {
            var result = _tourService.Update(tour);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id) 
        {
            var result = _tourService.Delete(id);
            return CreateResponse(result);
        }

        [HttpPut("addingCheckpoint/{id:int}")]
        public ActionResult<TourDto> AddCheckpoint(int id,[FromBody] CheckpointDto checkpoint)
        {
            if (!string.IsNullOrEmpty(checkpoint.ImageBase64))
            {
                var imageData = Convert.FromBase64String(checkpoint.ImageBase64.Split(',')[1]);
                var fileName = Guid.NewGuid() + ".png";
                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "checkpoints");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var filePath = Path.Combine(folderPath, fileName);
                System.IO.File.WriteAllBytes(filePath, imageData);
                checkpoint.ImageUrl = $"images/checkpoints/{fileName}";
            }
            var result = _tourService.AddCheckpoint(id,checkpoint);
            return CreateResponse(result);
        }

        [HttpPut("editingCheckpoint/{id:int}")]
        public ActionResult<TourDto> UpdateCheckpoint(int id, [FromBody] CheckpointDto checkpoint)
        {
            if (!string.IsNullOrEmpty(checkpoint.ImageUrl))
            {
                var oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, checkpoint.ImageUrl);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            if (!string.IsNullOrEmpty(checkpoint.ImageBase64))
            {
                var imageData = Convert.FromBase64String(checkpoint.ImageBase64.Split(',')[1]);
                var fileName = Guid.NewGuid() + ".png";
                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "checkpoints");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var filePath = Path.Combine(folderPath, fileName);
                System.IO.File.WriteAllBytes(filePath, imageData);
                checkpoint.ImageUrl = $"images/checkpoints/{fileName}";
            }

            var result = _tourService.UpdateCheckpoint(id, checkpoint);
            return CreateResponse(result);
        }

        [HttpPut("deletingCheckpoint/{id:int}")]
        public ActionResult DeleteCheckpoint(int id, [FromBody] CheckpointDto checkpoint)
        {
            var result = _tourService.DeleteCheckpoint(id,checkpoint);
            return CreateResponse(result);
        }

        [HttpPut("updatingLength/{id:int}")]
        public ActionResult<TourDto> UpdateLength(int id, [FromBody] double length)
        {
            var result = _tourService.UpdateLength(id, length);
            return CreateResponse(result);
        }

        [HttpPut("addingTransportDuration/{id:int}")]
        public ActionResult<TourDto> AddTransportDurations(int id, [FromBody] List<TransportDurationDto> transportDurations)
        {
            var result = _tourService.AddTransportDurations(id, transportDurations);
            return CreateResponse(result);
        }

        [HttpPut("changeStatus/{id:int}")]
        public ActionResult<TourDto> ChangeStatus(int id, [FromBody] string status)
        {
            FluentResults.Result<TourDto> result;

            if (status.Equals("Archive"))
            {
                result = _tourService.Archive(id);
            }
            else
            {
                result = _tourService.Publish(id);
            }

            return CreateResponse(result);
        }
    }
}
