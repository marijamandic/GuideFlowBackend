using System.Security.Claims;
using Explorer.API.Controllers;
using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.ProfileInfo;
using Microsoft.AspNetCore.Hosting;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Tours.Infrastructure.Database;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Stakeholders.Tests.Integration
{
    [Collection("Sequential")]
    public class UserProfileTests : BaseStakeholdersIntegrationTest
    {
        public UserProfileTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var newEntity = new ProfileInfoDto
            {
                // Id = -28,
                UserId = -23,
                FirstName = "pera",
                LastName = "peric",
                ImageUrl = "images/profileInfo/6e154d0e-a0e4-4a42-8e0d-dce188f21532.png",
                //ImageBase64 = null,
                Biography = "bio",
                Moto = "moto"
            };

            // Act
            var response = ((ObjectResult)controller.Create(newEntity).Result)?.Value as ProfileInfoDto;

            // Assert - Response
            response.ShouldNotBeNull();
            //response.Id.ShouldBe(newEntity.Id); //ovo sam sa kristinom radio
            response.UserId.ShouldBe(newEntity.UserId);
            response.FirstName.ShouldBe(newEntity.FirstName);
            response.LastName.ShouldBe(newEntity.LastName);
            //response.ProfilePicture.ShouldBe(newEntity.ProfilePicture); 
            response.Biography.ShouldBe(newEntity.Biography);
            response.Moto.ShouldBe(newEntity.Moto);

            // Assert - Database
            var storedEntity = dbContext.Profiles.FirstOrDefault(i => i.UserId == newEntity.UserId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(response.Id); //proverim samo da li je to onaj id koji je napravio create iz crud
        }

        private static ProfileInfoController CreateController(IServiceScope scope)
        {
            return new ProfileInfoController(scope.ServiceProvider.GetRequiredService<IProfileInfoService>(), scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}