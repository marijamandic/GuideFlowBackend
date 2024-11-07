﻿using Explorer.API.Controllers.Administrator.Administration;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Public;

namespace Explorer.Tours.Tests.Integration
{
    public class EquipmentManagementQueryTests : BaseToursIntegrationTest
    {
        public EquipmentManagementQueryTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<EquipmentManagementDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(4);
            result.TotalCount.ShouldBe(4);
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
