using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
namespace Explorer.API.Controllers.Author.BlogManagement
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/blogManagement/post")]
    public class PostController : BaseApiController
    {
        private readonly IPostService _postService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PostController(IPostService postService , IWebHostEnvironment webHostEnvironment) {
            _postService = postService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public ActionResult<PagedResult<PostDto>> GetAll([FromQuery]int page, [FromQuery]int pageSize) {
            var result = _postService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<PostDto> Create([FromBody] PostDto post) {

            if (!string.IsNullOrEmpty(post.ImageBase64)){
                var imageData = Convert.FromBase64String(post.ImageBase64.Split(',')[1]);
                var fileName = Guid.NewGuid() + ".png"; // ili bilo koji format slike
                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "blogs");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var filePath = Path.Combine(folderPath, fileName);
                System.IO.File.WriteAllBytes(filePath, imageData);
                post.ImageUrl = $"images/blogs/{fileName}";
            }


            var result = _postService.Create(post);
            return CreateResponse(result);
        }
    }
}
