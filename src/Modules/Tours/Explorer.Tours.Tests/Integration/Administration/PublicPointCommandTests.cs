using Explorer.API.Controllers.Author;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace Explorer.Tours.Tests.Integration.Administration
{
    [Collection("Sequential")]
    public class PublicPointCommandTests : BaseToursIntegrationTest
    {
        public PublicPointCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates_PublicPoint()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newPublicPoint = new PublicPointDto
            {
                Name = "Tourist Spot",
                Description = "A beautiful tourist spot.",
                Latitude = 45.2671,
                Longitude = 19.8335,
                ImageUrl = "/images/tourist-spot.jpg",
                ImageBase64 = "",
                ApprovalStatus = ApprovalStatus.Pending,
                PointType = PointType.Checkpoint,
                AuthorId = 1
            };

            // Act
            var result = ((ObjectResult)controller.Create(newPublicPoint).Result)?.Value as PublicPointDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Name.ShouldBe(newPublicPoint.Name);

            // Assert - Database
            var storedEntity = dbContext.PublicPoints.FirstOrDefault(i => i.Name == newPublicPoint.Name);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Create_PublicPoint_Fails_Invalid_Data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var invalidPublicPoint = new PublicPointDto
            {
                // Missing required fields
                Description = "Test description"
            };

            // Act
            var result = (ObjectResult)controller.Create(invalidPublicPoint).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }

        [Fact]
        public void Updates_PublicPoint()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            var updatedPublicPoint = new PublicPointDto
            {
                Id = -2, // Existing ID
                Name = "Updated Tourist Spot",
                Description = "Updated description.",
                Latitude = 5.2700,
                Longitude = 19.8400,
                ImageUrl = "/images/updated-tourist-spot.jpg",
                ImageBase64 = "",
                ApprovalStatus = ApprovalStatus.Pending,
                PointType = PointType.Checkpoint,
                AuthorId = 1
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedPublicPoint.Id, updatedPublicPoint).Result)?.Value as PublicPointDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-2);
            result.Name.ShouldBe(updatedPublicPoint.Name);

            // Assert - Database
            var storedEntity = dbContext.PublicPoints.FirstOrDefault(i => i.Id == -2);
            storedEntity.ShouldNotBeNull();
            storedEntity.Description.ShouldBe(updatedPublicPoint.Description);
        }


        [Fact]
        public void Update_PublicPoint_Fails_Invalid_Id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var invalidPublicPoint = new PublicPointDto
            {
                Id = -1000, // Invalid Id
                Name = "Updated Tourist Spot",
                Description = "Updated description.",
                Latitude = 5.2700,
                Longitude = 19.8400,
                ImageUrl = "/images/updated-tourist-spot.jpg",
                ImageBase64 = "",
                ApprovalStatus = ApprovalStatus.Pending,
                PointType = PointType.Checkpoint,
                AuthorId = 1
            };

            // Act
            var result = (ObjectResult)controller.Update(invalidPublicPoint.Id, invalidPublicPoint).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        [Fact]
        public void Deletes_PublicPoint()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = (OkResult)controller.Delete(-1); 


            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedEntity = dbContext.PublicPoints.FirstOrDefault(i => i.Id == -1);
            storedEntity.ShouldBeNull();
        }

        [Fact]
        public void Delete_PublicPoint_Fails_Invalid_Id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = (ObjectResult)controller.Delete(-1000); // Invalid Id

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        [Fact]
        public void Gets_PublicPoint()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.Get(-2).Result)?.Value as PublicPointDto;

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-2); // Assuming 1 is a valid ID
        }

        [Fact]
        public void Get_PublicPoint_Fails_Invalid_Id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var actionResult = controller.Get(-1000); // Invalid Id

            // Assert
            var result = actionResult.Result; // Get the ActionResult
            result.ShouldBeOfType<NotFoundResult>(); // Assert that the result is NotFound
        }



        private static PublicPointController CreateController(IServiceScope scope)
        {
            return new PublicPointController(scope.ServiceProvider.GetRequiredService<IPublicPointService>(), scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
