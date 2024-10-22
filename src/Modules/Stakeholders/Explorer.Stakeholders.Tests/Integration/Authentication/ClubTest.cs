using Explorer.API.Controllers.Tourist;
using Explorer.Blog.Infrastructure.Database;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Dtos.Club;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.API.Public.Club;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Tests.Integration.Authentication
{
    [Collection("Sequential")]
    public class ClubTests : BaseStakeholdersIntegrationTest
    {
        public ClubTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            var newEntity = new ClubDto
            {
                Name = "jedan club",
                OwnerId = 1,
                Description = "nema",
                ImageUrl = "string",
            };
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as ClubDto;

            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);

            var storedEntity = dbContext.Clubs.FirstOrDefault(i => i.Name == newEntity.Name);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }
        [Fact]
        public void Updates()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            var updatedEntity = new ClubDto
            {
                Id = -10,
                Name = "drugi club",
                OwnerId = 10,
                Description = "nema",
                ImageUrl = "string",
                ImageBase64 = "string"
            };
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as ClubDto;

            result.ShouldNotBeNull();
            result.Id.ShouldBe(updatedEntity.Id);
            result.Name.ShouldBe(updatedEntity.Name);
            result.Description.ShouldBe(updatedEntity.Description);

            var storedEntity = dbContext.Clubs.FirstOrDefault(i => i.Name == updatedEntity.Name);
            storedEntity.ShouldNotBeNull();
            storedEntity.Description.ShouldBe(updatedEntity.Description);
            var oldEntity = dbContext.Clubs.FirstOrDefault(i => i.Name == "Club_1");
            oldEntity.ShouldBeNull();
        }
        [Fact]
        public void Deletes()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            var result = (OkResult)controller.Delete(-20);

            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            var storedCourse = dbContext.Clubs.FirstOrDefault(i => i.Id == -20);
            storedCourse.ShouldBeNull();
        }

        private static ClubController CreateController(IServiceScope scope)
        {
            return new ClubController(scope.ServiceProvider.GetRequiredService<IClubService>(), scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}