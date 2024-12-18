using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.API.Public.Club;
using Explorer.Stakeholders.Core.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Route("api/manageauthor/dashboard")]
    public class AuthorDashboardController : BaseApiController
    {
        private readonly IAuthorDashboardService _authorDashboardService;

        public AuthorDashboardController(IAuthorDashboardService authorDashboardService)
        {
            _authorDashboardService = authorDashboardService;
        }

        [HttpGet("{authorId}/average-grade")]
        public ActionResult<double> GetAverageGrade(long authorId)
        {
            var result = _authorDashboardService.GetAverageGradeForAuthor(authorId);
            return Ok(result);
        }

        [HttpGet("{authorId}/reviews-partition")]
        public ActionResult<Dictionary<int, int>> GetReviewsPartitioned(long authorId)
        {
            var result = _authorDashboardService.GetReviewsPartitionedByGrade(authorId);
            return Ok(result);
        }
    }
}
