using Explorer.API.Controllers.Tourist.CommentManaging;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public.Aggregate_service_interface;
using Explorer.Blog.Infrastructure.Database;
using Explorer.Stakeholders.API.Public;
using FluentResults;
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
        
        [Fact]
        public void Creates()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var newEntity = new CommentDto
            {
                UserId = -11,
                PostId = -1,
                CreatedAt = DateTime.UtcNow,
                Content = "na najjace",
                LastModified = DateTime.UtcNow
            };

            // Act
            var actionResult = controller.Create(newEntity).Result;
            var objectResult = actionResult as ObjectResult;

            // Verify ObjectResult
            objectResult.ShouldNotBeNull("Expected an ObjectResult but got null.");
            Console.WriteLine($"Status Code: {objectResult?.StatusCode}");

            if (objectResult?.StatusCode != 200)
            {
                Console.WriteLine($"Error Message: {objectResult?.Value}");
                Assert.Fail($"Expected status code 200 but received {objectResult?.StatusCode} with message: {objectResult?.Value}");
            }

            var result = objectResult.Value as CommentDto;
            result.ShouldNotBeNull("Expected result to be a valid CommentDto, but it was null.");
            result.Id.ShouldNotBe(0);
            result.UserId.ShouldBe(newEntity.UserId);

            var storedEntity = dbContext.Comments.FirstOrDefault(i => i.Content == newEntity.Content);
            storedEntity.ShouldNotBeNull("Expected post to be stored in the database, but it was not found.");
            storedEntity.Id.ShouldBe(result.Id);
            storedEntity.UserId.ShouldBe(newEntity.UserId);
            storedEntity.Content.ShouldBe(newEntity.Content);
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

            var actionResult = controller.Create(updatedEntity).Result;

            // Check if the ActionResult is an ObjectResult
            if (actionResult is ObjectResult objectResult)
            {
                Console.WriteLine($"Status Code: {objectResult.StatusCode}");
                objectResult.StatusCode.ShouldBe(400, "Expected status code 400 for invalid data.");
            }
            else
            {
                Console.WriteLine("Create action with invalid data did not return an ObjectResult.");
                Assert.Fail("Expected an ObjectResult but got a different result type.");
            }
        }

        [Fact]
        public void Updates()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

            // Retrieve the existing comment to ensure the CreatedAt date matches
            var existingComment = dbContext.Comments.FirstOrDefault(c => c.Id == -1);
            if (existingComment == null)
            {
                Console.WriteLine("The comment with Id = -1 does not exist in the database.");
                Assert.Fail("Required comment not found in the database.");
            }

            var updatedEntity = new CommentDto
            {
                Id = -1,
                UserId = -21,
                PostId = -1,
                CreatedAt = existingComment.CreatedAt,
                Content = "iskr nije nes",
                LastModified = DateTime.UtcNow
            };

            // Act
            var actionResult = controller.Update(updatedEntity, updatedEntity.Id);

            var objectResult = actionResult.Result as ObjectResult;

            if (objectResult == null)
            {
                Console.WriteLine("Update action did not return an ObjectResult.");
                Assert.Fail("Expected an ObjectResult but got a different result type.");
            }
            else
            {
                Console.WriteLine($"Status Code: {objectResult.StatusCode}");

                // Access the CommentDto in the ObjectResult
                var result = objectResult.Value as CommentDto;
                result.ShouldNotBeNull("Expected result to be a valid CommentDto, but it was null.");
                result.Id.ShouldBe(-1);
                result.UserId.ShouldBe(updatedEntity.UserId);
                result.PostId.ShouldBe(updatedEntity.PostId);
                result.CreatedAt.ShouldBe(updatedEntity.CreatedAt);
                result.Content.ShouldBe(updatedEntity.Content);
                result.LastModified.ShouldBe(updatedEntity.LastModified, TimeSpan.FromMilliseconds(50));
            }
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
            var objectResult = actionResult.Result as ObjectResult;

            // Check if the action result is an ObjectResult and log the status code
            if (objectResult == null)
            {
                Console.WriteLine("Update action with invalid ID did not return an ObjectResult.");
                Assert.Fail("Expected an ObjectResult but got a different result type.");
            }
            else
            {
                Console.WriteLine($"Status Code: {objectResult.StatusCode}");

                
                objectResult.StatusCode.ShouldBe(400, "Expected status code 404 for invalid ID.");
            }
        }

        [Fact]
        public void Deletes()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

            var actionResult = controller.Delete(-3);
            var result = actionResult as OkResult;

            if (result == null)
            {
                Console.WriteLine("Delete action did not return an OkResult.");
            }

            result.ShouldNotBeNull("Expected OkResult but got null.");
            result.StatusCode.ShouldBe(200);
        }

       

        private static CommentController CreateController(IServiceScope scope)
        {
            return new CommentController(scope.ServiceProvider.GetRequiredService<IPostAggregateService>(), scope.ServiceProvider.GetRequiredService<IUserService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
