using Explorer.API.Controllers.Tourist;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Tests.Integration
{
    [Collection("Sequential")]
    public class NewAppRatingTests : BaseStakeholdersIntegrationTest
    {
        public NewAppRatingTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Successfully_rated_the_app()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var newEntity = new RatingAppDto
            {
                UserId = 11,
                RatingValue = 5,
                Comment = "TEST",
                RatingTime = DateTime.UtcNow.AddSeconds(-1)
            };


            //Act
            var actionResult = controller.NewAppRating(newEntity).Result as ObjectResult;
            var result = actionResult?.Value as RatingAppDto;

            //Assert
            result.ShouldNotBeNull();

            var storedEntity = dbContext.Ratings.FirstOrDefault(i => i.Comment == newEntity.Comment);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        private static AppRatingController CreateController(IServiceScope scope)
        {
            return new AppRatingController(scope.ServiceProvider.GetRequiredService<IRatingAppService>());
        }
    }
}
