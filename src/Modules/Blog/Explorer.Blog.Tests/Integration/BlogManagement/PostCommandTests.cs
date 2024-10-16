using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Author.BlogManagement;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Infrastructure.Database;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Tests.Integration.BlogManagement
{
    [Collection("Sequential")]
    public class PostCommandTests : BaseBlogIntegrationTest
    {
        public PostCommandTests(BlogTestFactory factory) : base(factory) {}

        [Fact]
        public void Creates() {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var newEntity = new PostDto
            {
                Title = "drugi blog",
                UserId = -1,
                Description = "nema",
                PublishDate = DateTime.UtcNow,
                ImageUrl = "string",
                Status = 0
            };

            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as PostDto;
            
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);

            var storedEntity = dbContext.Posts.FirstOrDefault(i => i.Title == newEntity.Title);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }



        private static PostController CreateController(IServiceScope scope)
        {
            return new PostController(scope.ServiceProvider.GetRequiredService<IPostService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
