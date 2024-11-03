using Explorer.API.Controllers.Tourist.CommentManaging;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public.Aggregate_service_interface;
using Explorer.Blog.Infrastructure.Database;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using Xunit;

namespace Explorer.Blog.Tests.Integration.CommentManaging
{
    [Collection("Sequential")]
    public class CommentCommandTest : BaseBlogIntegrationTest
    {
        public CommentCommandTest(BlogTestFactory factory) : base(factory) { }
        /*
        [Fact]
        public void Creates()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var newEntity = new CommentDto
            {
                UserId = -2,
                PostId = -1,
                CreatedAt = DateTime.UtcNow,
                Content = "na najjace",
                LastModified = DateTime.UtcNow
            };

            // Act
            var actionResult = controller.Create(newEntity);
            var objectResult = actionResult as ObjectResult;

            // Logging for troubleshooting
            if (objectResult == null)
            {
                Console.WriteLine("Create action did not return an ObjectResult.");
            }
            else
            {
                Console.WriteLine($"Status Code: {objectResult.StatusCode}");
            }

            var result = objectResult?.Value as CommentDto;
            result.ShouldNotBeNull("Expected result to be a valid CommentDto, but it was null.");
            result.Id.ShouldNotBe(0);
            result.UserId.ShouldBe(newEntity.UserId);
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new CommentDto
            {
                UserId = -2,
                CreatedAt = DateTime.UtcNow,
                Content = "na najjace",
                LastModified = DateTime.UtcNow
            };

            var actionResult = controller.Create(updatedEntity);
            var objectResult = actionResult as ObjectResult;

            // Logging for troubleshooting
            if (objectResult == null)
            {
                Console.WriteLine("Create action with invalid data did not return an ObjectResult.");
            }
            else
            {
                Console.WriteLine($"Status Code: {objectResult.StatusCode}");
            }

            objectResult.ShouldNotBeNull("Expected ObjectResult but got null.");
            objectResult.StatusCode.ShouldBe(400);
        }

        [Fact]
        public void Updates()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var updatedEntity = new CommentDto
            {
                Id = -1,
                UserId = -2,
                PostId = -1,
                CreatedAt = DateTime.UtcNow,
                Content = "iskr nije nes",
                LastModified = DateTime.UtcNow
            };

            var actionResult = controller.Update(updatedEntity, updatedEntity.Id);
            var objectResult = actionResult as ObjectResult;

            if (objectResult == null)
            {
                Console.WriteLine("Update action did not return an ObjectResult.");
            }
            else
            {
                Console.WriteLine($"Status Code: {objectResult.StatusCode}");
            }

            var result = objectResult?.Value as CommentDto;
            result.ShouldNotBeNull("Expected result to be a valid CommentDto, but it was null.");
            result.Id.ShouldBe(-1);
            result.UserId.ShouldBe(updatedEntity.UserId);
            result.PostId.ShouldBe(updatedEntity.PostId);
            result.CreatedAt.ShouldBe(updatedEntity.CreatedAt);
            result.Content.ShouldBe(updatedEntity.Content);
            result.LastModified.ShouldBe(updatedEntity.LastModified);
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new CommentDto
            {
                Id = -1000,
                UserId = -2,
                PostId = -1,
                CreatedAt = DateTime.UtcNow,
                Content = "mama mia",
                LastModified = DateTime.UtcNow
            };

            var actionResult = controller.Update(updatedEntity, updatedEntity.Id);
            var objectResult = actionResult as ObjectResult;

            if (objectResult == null)
            {
                Console.WriteLine("Update action with invalid ID did not return an ObjectResult.");
            }
            else
            {
                Console.WriteLine($"Status Code: {objectResult.StatusCode}");
            }

            objectResult.ShouldNotBeNull("Expected ObjectResult but got null.");
            objectResult.StatusCode.ShouldBe(404);
        }

        [Fact]
        public void Deletes()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

            var actionResult = controller.Delete(-3, -2, DateTime.UtcNow);
            var result = actionResult as OkResult;

            if (result == null)
            {
                Console.WriteLine("Delete action did not return an OkResult.");
            }

            result.ShouldNotBeNull("Expected OkResult but got null.");
            result.StatusCode.ShouldBe(200);
        }

        [Fact]
        public void Delete_fails_invalid_id()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            var actionResult = controller.Delete(-1000, -2, DateTime.UtcNow);
            var objectResult = actionResult as ObjectResult;

            if (objectResult == null)
            {
                Console.WriteLine("Delete action with invalid ID did not return an ObjectResult.");
            }
            else
            {
                Console.WriteLine($"Status Code: {objectResult.StatusCode}");
            }

            objectResult.ShouldNotBeNull("Expected ObjectResult but got null.");
            objectResult.StatusCode.ShouldBe(404);
        }*/

        private static CommentController CreateController(IServiceScope scope)
        {
            return new CommentController(scope.ServiceProvider.GetRequiredService<IPostAggregateService>(), scope.ServiceProvider.GetRequiredService<IUserService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
