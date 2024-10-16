using Explorer.API.Controllers.Author.Management;
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

namespace Explorer.Tours.Tests.Integration.Administration
{
    [Collection("Sequential")]
    public class TourEquipmentCommandTests : BaseToursIntegrationTest
    {
        public TourEquipmentCommandTests(ToursTestFactory factory) : base(factory) { }
        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newEntity = new TourEquipmentDto
            {
                TourId = 2,
                EquipmentId = 3,
                Quantity = 5
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourEquipmentDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.TourId.ShouldBe(newEntity.TourId);
            result.EquipmentId.ShouldBe(newEntity.EquipmentId);
            result.Quantity.ShouldBe(newEntity.Quantity);

            // Assert - Database
            var storedEntity = dbContext.TourEquipment.FirstOrDefault(i => i.TourId == newEntity.TourId && i.EquipmentId == newEntity.EquipmentId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var newEntity = new TourEquipmentDto
            {
                Quantity = 0 // Invalid quantity
            };

            // Act
            var result = (ObjectResult)controller.Create(newEntity).Result;

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
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var updatedEntity = new TourEquipmentDto
            {
                Id = -1,
                TourId = 2,
                EquipmentId = 3,
                Quantity = 10
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as TourEquipmentDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);
            result.TourId.ShouldBe(updatedEntity.TourId);
            result.EquipmentId.ShouldBe(updatedEntity.EquipmentId);
            result.Quantity.ShouldBe(updatedEntity.Quantity);

            // Assert - Database
            var storedEntity = dbContext.TourEquipment.FirstOrDefault(i => i.Id == updatedEntity.Id);
            storedEntity.ShouldNotBeNull();
            storedEntity.Quantity.ShouldBe(updatedEntity.Quantity);
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new TourEquipmentDto
            {
                Id = -1000, // Invalid Id
                TourId = 2,
                EquipmentId = 3,
                Quantity = 10
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
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = (OkResult)controller.Delete(-3);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedEntity = dbContext.TourEquipment.FirstOrDefault(i => i.Id == -3);
            storedEntity.ShouldBeNull();
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

        private static TourEquipmentController CreateController(IServiceScope scope)
        {
            return new TourEquipmentController(scope.ServiceProvider.GetRequiredService<ITourEquipmentService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }


    }
}
