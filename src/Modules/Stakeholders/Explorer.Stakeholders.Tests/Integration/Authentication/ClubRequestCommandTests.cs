﻿using Explorer.API.Controllers;
using Explorer.API.Controllers.Tourist;
using Explorer.Stakeholders.API.Dtos.Club;
using Explorer.Stakeholders.API.Public.Club;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Tests.Integration.Authentication
{
    [Collection("Sequential")]
    public class ClubRequestCommandTests : BaseStakeholdersIntegrationTest
    {
        public ClubRequestCommandTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var newEntity = new ClubRequestDto
            {
                TouristId = 1,
                ClubId = 1,
                Status = 0
            };

            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as ClubRequestDto;
            
            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(0);
            result.TouristId.ShouldNotBe(0);
            result.ClubId.ShouldNotBe(0);
            result.Status.ShouldBe(newEntity.Status);

            // Assert - Database
            var storedEntity = dbContext.ClubRequests.FirstOrDefault(i => i.ClubId == newEntity.ClubId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(newEntity.Id);
        }

        private static ClubRequestController CreateController(IServiceScope scope)
        {
            return new ClubRequestController(scope.ServiceProvider.GetRequiredService<IClubRequestService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
