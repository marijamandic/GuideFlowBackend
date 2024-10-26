using Explorer.API.Controllers.Tourist;
using Explorer.Stakeholders.API.Dtos.Club;
using Explorer.Stakeholders.API.Public.Club;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Explorer.Stakeholders.Tests.Integration.Authentication
{
    [Collection("Sequential")]
    public class ClubMemberTests : BaseStakeholdersIntegrationTest
    {
        public ClubMemberTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_All_Members()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            long clubId = 1;

            var result = ((ObjectResult)controller.GetAllMembers(clubId).Result)?.Value as IEnumerable<ClubMemberDto>;

            result.ShouldNotBeNull();
            result.Count().ShouldBe(1);
        }

        [Fact]
        public void AddMember_AddsNewMember()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var newMember = new ClubMemberDto
            {
                ClubId = 1,
                UserId = 1
            };

            var result = ((ObjectResult)controller.AddMember(newMember).Result)?.Value as ClubMemberDto;

            result.ShouldNotBeNull();
            result.ClubId.ShouldBe(newMember.ClubId);
            result.UserId.ShouldBe(newMember.UserId);
        }

        [Fact]
        public void RemoveMember_RemovesMember()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            long clubId = 5;
            long userId = 5;
            var result = (OkResult)controller.RemoveMember(clubId, userId);


            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
            var storedCourse = dbContext.ClubMembers.FirstOrDefault(i => i.Id == -30);
            storedCourse.ShouldBeNull();
        }

        private static ClubMemberController CreateController(IServiceScope scope)
        {
            return new ClubMemberController(scope.ServiceProvider.GetRequiredService<IClubMemberService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
