using Explorer.Stakeholders.Core.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [ApiController]
    [Route("api/author-dashboard")]
    public class AuthorDashboardController : ControllerBase
    {
        private readonly AuthorDashboardService _authorDashboardService;

        public AuthorDashboardController(AuthorDashboardService authorDashboardService)
        {
            _authorDashboardService = authorDashboardService;
        }

        [HttpGet("{authorId}/average-grade")]
        public IActionResult GetAverageGrade(long authorId)
        {
            var averageGrade = _authorDashboardService.GetAverageGradeForAuthor(authorId);
            return Ok(new { AverageGrade = averageGrade });
        }

        [HttpGet("{authorId}/reviews-partition")]
        public IActionResult GetReviewsPartitioned(long authorId)
        {
            var partitionedReviews = _authorDashboardService.GetReviewsPartitionedByGrade(authorId);
            return Ok(partitionedReviews);
        }
    }
}
