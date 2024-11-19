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
    [Fact]
    public void Update()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
        var entity = new ProbStatusChangeDto
        {
            IsSolved = true,
            TouristMessage = "uspesno promenjeno"
        };
        var result = ((ObjectResult)controller.Update(-2,entity).Result)?.Value as ProblemDto;
        result.ShouldNotBeNull();
        result.Id.ShouldBe(-2);
        result.Resolution.IsResolved.ShouldBe(true);

        var storedEntity = dbContext.Messages.FirstOrDefault(i => i.Content == "uspesno promenjeno");
        storedEntity.ShouldNotBeNull();
        storedEntity.ProblemId.ShouldBe(-2);
    }
    //[Fact]
    //public void UpdateDeadline()
    //{
    //    using var scope = Factory.Services.CreateScope();
    //    var controller = CreateAdminController(scope);
    //    var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
    //    var entity = new DeadlineDto
    //    {
    //        Date = DateTime.UtcNow.AddDays(10)
    //    };
    //    var result = ((ObjectResult)controller.UpdateDeadline(-2, entity).Result)?.Value as ProblemDto;
    //    result.ShouldNotBeNull();
    //    result.Id.ShouldBe(-2);
    //    result.Resolution.Deadline.ShouldBe(DateTime.UtcNow.AddDays(10), TimeSpan.FromSeconds(10));
    //}

    private static ProblemController CreateController(IServiceScope scope)
    {
        return new ProblemController(scope.ServiceProvider.GetRequiredService<IProblemService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
    private static Explorer.API.Controllers.Administrator.Administration.ProblemController CreateAdminController(IServiceScope scope)
    {
        return new Explorer.API.Controllers.Administrator.Administration.ProblemController(scope.ServiceProvider.GetRequiredService<IProblemService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
