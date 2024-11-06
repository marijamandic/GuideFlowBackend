using Explorer.API.Controllers.Tourist;
using Explorer.Stakeholders.API.Dtos.Problems;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Stakeholders.Tests.Integration;
public class ProblemCommandTests : BaseStakeholdersIntegrationTest
{
    public ProblemCommandTests(StakeholdersTestFactory factory) : base(factory) { }

    [Fact]
    public void Creates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
        var newEntity = new CreateProblemInputDto
        {
            UserId = 1,
            TourId = 2,
            Category = ProblemCategory.Accommodation,
            Priority = ProblemPriority.Medium,
            Description = "unique description"
        };

        // Act
        var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as ProblemDto;

        // Assert - response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0);
        result.Details.Category.ShouldBe(newEntity.Category);

        // Assert - db
        var storedEntity = dbContext.Problems.FirstOrDefault(p => p.Details.Description == result.Details.Description);
        storedEntity.ShouldNotBeNull();
        storedEntity.Id.ShouldBe(result.Id);
    }

    private static ProblemController CreateController(IServiceScope scope)
    {
        return new ProblemController(scope.ServiceProvider.GetRequiredService<IProblemService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
