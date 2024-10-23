using Explorer.API.Controllers.Tourist;
using Explorer.Stakeholders.API.Dtos.Club;
using Explorer.Stakeholders.API.Public.Club;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Explorer.Stakeholders.Tests.Integration.Authentication
{
    [Collection("Sequential")]
    public class ClubInvitationTests : BaseStakeholdersIntegrationTest
    {
        public ClubInvitationTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_All_Invitations()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            var result = ((ObjectResult)controller.GetAll().Result)?.Value as IEnumerable<ClubInvitationDto>;

            result.ShouldNotBeNull();
            result.Count().ShouldBe(4);
        }

        [Fact]
        public void GetInvitation_ReturnsInvitationById()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            int invitationId = -1;

            var result = ((ObjectResult)controller.GetInvitation(invitationId).Result)?.Value as ClubInvitationDto;

            result.ShouldNotBeNull();
            result.Id.ShouldBe(invitationId);
        }

        [Fact]
        public void Accept_AcceptsInvitation()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            int invitationId = -2;

            var result = ((ObjectResult)controller.Accept(invitationId).Result)?.Value as ClubInvitationDto;

            result.ShouldNotBeNull();
            result.Status.ShouldBe(ClubInvitationStatus.ACCEPTED);
        }

        [Fact]
        public void Decline_DeclinesInvitation()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            int invitationId = -3;

            var result = ((ObjectResult)controller.Decline(invitationId).Result)?.Value as ClubInvitationDto;

            result.ShouldNotBeNull();
            result.Status.ShouldBe(ClubInvitationStatus.DECLINED);
        }

        [Fact]
        public void Cancel_CancelsInvitation()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            int invitationId = -4;

            var result = controller.Cancel(invitationId) as ObjectResult;

            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
        }

        private static ClubInvitationController CreateController(IServiceScope scope)
        {
            return new ClubInvitationController(scope.ServiceProvider.GetRequiredService<IClubInvitationService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
