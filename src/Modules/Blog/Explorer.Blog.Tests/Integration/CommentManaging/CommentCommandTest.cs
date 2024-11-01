using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Tourist.CommentManaging;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Infrastructure.Database;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Tests.Integration.CommentManaging
{
    [Collection("Sequential")]
    public class CommentCommandTest:BaseBlogIntegrationTest
    {
        public CommentCommandTest(BlogTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
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
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as CommentDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.UserId.ShouldBe(newEntity.UserId);


            // Assert - Database
            //var storedEntity = dbContext.Comments.FirstOrDefault(i => i.Content == newEntity.Content);
            //storedEntity.ShouldNotBeNull();
            //storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new CommentDto
            {
                UserId = -2,
                CreatedAt = DateTime.UtcNow,
                Content = "na najjace",
                LastModified = DateTime.UtcNow
            };

            // Act
            var result = (ObjectResult)controller.Create(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }

        [Fact]
        public void Updates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var updatedEntity = new CommentDto
            {
                Id=-1,
                UserId = -2,
                PostId = -1,
                CreatedAt = DateTime.UtcNow,
                Content = "iskr nije nes",
                LastModified = DateTime.UtcNow 
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as CommentDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);
            result.UserId.ShouldBe(updatedEntity.UserId);
            result.PostId.ShouldBe(updatedEntity.PostId);
            result.CreatedAt.ShouldBe(updatedEntity.CreatedAt);
            result.Content.ShouldBe(updatedEntity.Content);
            result.LastModified.ShouldBe(updatedEntity.LastModified);

            // Assert - Database
            //var storedEntity = dbContext.Comments.FirstOrDefault(i => i.Content == "iskr nije nes");
            //storedEntity.ShouldNotBeNull();
            //storedEntity.UserId.ShouldBe(updatedEntity.UserId);
            //var oldEntity = dbContext.Comments.FirstOrDefault(i => i.Content == "top prica");
            //oldEntity.ShouldBeNull();
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
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

            // Act
            var result = (ObjectResult)controller.Update(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        [Fact]
        public void Deletes()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

            // Act
            var result = (OkResult)controller.Delete(-3);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            //var storedCourse = dbContext.Comments.FirstOrDefault(i => i.Id == -3);
            //storedCourse.ShouldBeNull();
        }

        [Fact]
        public void Delete_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = (ObjectResult)controller.Delete(-1000);

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        private static CommentController CreateController(IServiceScope scope)
        {
            return new CommentController(scope.ServiceProvider.GetRequiredService<ICommentService>(),scope.ServiceProvider.GetRequiredService<IUserService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }

    }
}
