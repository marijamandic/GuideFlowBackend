using Explorer.Tours.API.Dtos;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Explorer.API.Controllers.Administrator.Administration;
using Explorer.Tours.API.Public.Administration;
using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Public;
using Explorer.Blog.API.Dtos;

namespace Explorer.Tours.Tests.Integration
{
    public class EquipmentManagementCommandTests : BaseToursIntegrationTest
    {
        public EquipmentManagementCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newEntity = new EquipmentManagementDto
            {
                TouristId=1,
                EquipmentId=3,
                Status = 0
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as EquipmentManagementDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.EquipmentId.ShouldNotBe(0);
            result.TouristId.ShouldNotBe(0);
            //result.Status.ShouldBe(0);

            // Assert - Database
            var storedEntity = dbContext.EquipmentManagements.FirstOrDefault(i => i.EquipmentId == newEntity.EquipmentId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Delete_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = (ObjectResult)controller.DeleteEquipment(-5);

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        private static EquipmentManagementController CreateController(IServiceScope scope)
        {
            return new EquipmentManagementController(scope.ServiceProvider.GetRequiredService<IEquipmentManagementService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
    
}
