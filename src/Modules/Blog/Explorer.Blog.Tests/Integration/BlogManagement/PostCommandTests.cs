using Explorer.API.Controllers.Author.BlogManagement;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public.Aggregate_service_interface;
using Explorer.Blog.Core.Domain.Posts;
using Explorer.Blog.Infrastructure.Database;
using Explorer.Stakeholders.API.Dtos.Club;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using Xunit;

namespace Explorer.Blog.Tests.Integration.BlogManagement
{
    [Collection("Sequential")]
    public class PostCommandTests : BaseBlogIntegrationTest
    {
        public PostCommandTests(BlogTestFactory factory) : base(factory) {}
        public string ConvertImageToBase64(string imagePath)
        {
            byte[] imageArray = File.ReadAllBytes(imagePath);
            return Convert.ToBase64String(imageArray);
        }

       
        [Fact]
        public void Creates()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "blogs", "42e3228f-a504-42a1-9275-584b948975f2.png");
            string base64Image = ConvertImageToBase64(imagePath);

            var newEntity = new PostDto
            {
                Title = "drugi blog",
                UserId = -12,
                Description = "nema",
                PublishDate = DateTime.UtcNow,
                ImageUrl = "images/blogs/862f538f-4aab-4d1d-8a90-a22939135636.png",
                Status = API.Dtos.PostStatus.Draft,


            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as PostDto;


           /* // Logging for troubleshooting
            if (objectResult == null)
            {
                Console.WriteLine("Create action did not return an ObjectResult.");
            }
            else
            {
                Console.WriteLine($"Status Code: {objectResult.StatusCode}");
            }

            var result = objectResult?.Value as PostDto;*/
            result.ShouldNotBeNull("Expected result to be a valid PostDto, but it was null.");
            result.Id.ShouldNotBe(0);

            // Additional logging to confirm that the post is saved in the database
            var storedEntity = dbContext.Posts.FirstOrDefault(i => i.Title == newEntity.Title);
            storedEntity.ShouldNotBeNull("Expected post to be stored in the database, but it was not found.");
            storedEntity.Id.ShouldBe(result.Id);
        }

        private static PostController CreateController(IServiceScope scope)
        {
            return new PostController(scope.ServiceProvider.GetRequiredService<IPostAggregateService>(), scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
