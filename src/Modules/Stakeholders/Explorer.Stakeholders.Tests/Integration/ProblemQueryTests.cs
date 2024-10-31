using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Tourist;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Stakeholders.Tests.Integration;
public class ProblemQueryTests : BaseStakeholdersIntegrationTest
{
    public ProblemQueryTests(StakeholdersTestFactory factory) : base(factory) { }

    [Fact]
    public void Retreives_all()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateAdminController(scope);

        // Act
        var result = ((ObjectResult)controller.GetAll().Result)?.Value as PagedResult<ProblemDto>;

        // Assert
        result.ShouldNotBeNull();
    }

    private static Explorer.API.Controllers.Administrator.Administration.ProblemController CreateAdminController(IServiceScope scope)
    {
        return new Explorer.API.Controllers.Administrator.Administration.ProblemController(scope.ServiceProvider.GetRequiredService<IProblemService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }

    private static Explorer.API.Controllers.Tourist.ProblemController CreateTouristController(IServiceScope scope)
    {
        return new Explorer.API.Controllers.Tourist.ProblemController(scope.ServiceProvider.GetRequiredService<IProblemService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
