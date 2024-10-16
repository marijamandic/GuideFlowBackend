using Explorer.API.Controllers.Author.Management;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
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
    public class TourEquipmentQueryTests : BaseToursIntegrationTest
    {



        public TourEquipmentQueryTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<TourEquipmentDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(3); // Proveri očekivanu vrednost
            result.TotalCount.ShouldBe(3);    // Proveri očekivanu vrednost
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
