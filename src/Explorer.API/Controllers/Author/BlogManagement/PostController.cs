using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.API.Public.Aggregate_service_interface;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;
namespace Explorer.API.Controllers.Author.BlogManagement
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/blogManagement/post")]
    public class PostController : BaseApiController
    {
        private readonly IPostAggregateService _postAggregateService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PostController(IPostAggregateService postAggregateService, IWebHostEnvironment webHostEnvironment)
        {
            _postAggregateService = postAggregateService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public ActionResult<List<PostDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _postAggregateService.GetAllPosts(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<PostDto> Get(int id)
        {
            var result = _postAggregateService.GetPostById(id);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<PostDto> Create([FromBody] PostDto post)
        {
            if (!string.IsNullOrEmpty(post.ImageBase64))
            {
                var imageData = Convert.FromBase64String(post.ImageBase64.Split(',')[1]);
                var fileName = Guid.NewGuid() + ".png";
                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "blogs");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var filePath = Path.Combine(folderPath, fileName);
                System.IO.File.WriteAllBytes(filePath, imageData);
                post.ImageUrl = $"images/blogs/{fileName}";
            }

            var result = _postAggregateService.CreatePost(post);

            Debug.WriteLine("Service Result Success: " + result.IsSuccess);
            Debug.WriteLine("Created PostDto ID: " + result.Value?.Id);

            if (result.IsSuccess)
            {
                return Ok(result.Value); // Now returns the created PostDto
            }

            return BadRequest(result.Errors.FirstOrDefault()?.Message);

        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] PostDto post, int id)
        {
            post.Id = id;
            var result = _postAggregateService.UpdatePost(post);
            return CreateResponse(result);
        }
    }
}
