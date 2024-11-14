using Explorer.API.Controllers.Tourist.BlogRatingManaging;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public.Aggregate_service_interface;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Tests.Integration.BlogRatingManagement
{
    [Collection("Sequential")]
    public class BlogRatingTests : BaseBlogIntegrationTest
    {

        public BlogRatingTests(BlogTestFactory factory) : base(factory) { }


        [Fact]
        public void Create_AddsBlogRating()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var blogRatingDto = new BlogRatingDto
            {
                UserId = -21,
                PostId = -1,
                RatingDate = DateTime.UtcNow,
                RatingStatus = RatingStatus.Plus
            };

            // Act
            var actionResult = controller.Create(blogRatingDto).Result;
            var objectResult = actionResult as ObjectResult;

            // Assert
            objectResult.ShouldNotBeNull("Expected an ObjectResult but got null.");
            objectResult.StatusCode.ShouldBe(200);

            var result = objectResult.Value as BlogRatingDto;
            result.ShouldNotBeNull();
            result.UserId.ShouldBe(blogRatingDto.UserId);
            result.PostId.ShouldBe(blogRatingDto.PostId);
            result.RatingStatus.ShouldBe(blogRatingDto.RatingStatus);
        }

        [Fact]
        public void GetAllRatingsForPost()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            long postId = -1;

            // Act
            var actionResult = controller.GetAllRatingsForPost(postId).Result;
            var objectResult = actionResult as ObjectResult;

            // Assert
            objectResult.ShouldNotBeNull("Expected an ObjectResult but got null.");
            objectResult.StatusCode.ShouldBe(200);

            var ratings = objectResult.Value as List<BlogRatingDto>;
            ratings.ShouldNotBeNull();
            ratings.ShouldAllBe(r => r.PostId == postId);
        }

        [Fact]
        public void Delete_RemovesRating()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            long userId = -21;
            long postId = -1;

            // Act
            var actionResult = controller.Delete(userId, postId);
            // Assert
            if (actionResult is OkResult okResult)
            {
                // Successful deletion
                okResult.StatusCode.ShouldBe(200);
            }
            else if (actionResult is ObjectResult objectResult)
            {
                // Check for specific object result status code
                objectResult.StatusCode.ShouldBe(200);
            }
            else
            {
                Assert.Fail("Expected an OkResult or ObjectResult but got a different result type.");
            }
        }

        private static BlogRatingController CreateController(IServiceScope scope)
        {
            return new BlogRatingController(scope.ServiceProvider.GetRequiredService<IPostAggregateService>(), scope.ServiceProvider.GetRequiredService<IUserService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
