using Explorer.API.Controllers.Tourist.CommentManaging;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public.Aggregate_service_interface;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using Xunit;

namespace Explorer.Blog.Tests.Integration.CommentManaging
{
    [Collection("Sequential")]
    public class CommentQueryTests : BaseBlogIntegrationTest
    {
        public CommentQueryTests(BlogTestFactory factory) : base(factory) { }

        
        [Fact]
        public void Retrieves_all()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var actionResult = controller.GetAllCommentsForPost(-1);
            var okResult = actionResult.Result as OkObjectResult;

            // Logging for troubleshooting
            if (okResult == null)
            {
                Console.WriteLine("GetAllForPost action did not return an OkObjectResult.");
            }
            else
            {
                Console.WriteLine($"Status Code: {okResult.StatusCode}");
            }

            var result = okResult?.Value as List<CommentDto>;

            result.ShouldNotBeNull("Expected result to be a valid List<CommentDto>, but it was null.");
            result.ShouldNotBeEmpty("Expected at least one comment in the results, but it was empty.");
        }
        
        private static CommentController CreateController(IServiceScope scope)
        {
            return new CommentController(scope.ServiceProvider.GetRequiredService<IPostAggregateService>(), scope.ServiceProvider.GetRequiredService<IUserService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
        
    }
}
