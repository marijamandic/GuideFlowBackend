using Explorer.Payments.API.Dtos.Payments;
using Explorer.Payments.API.Public;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.API.Public.Club;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Route("api/manageauthor/dashboard")]
    public class AuthorDashboardController : BaseApiController
    {
        private readonly IAuthorDashboardService _authorDashboardService;
        private readonly IPaymentService _paymentService;

        public AuthorDashboardController(IAuthorDashboardService authorDashboardService, IPaymentService paymentService)
        {
            _authorDashboardService = authorDashboardService;
            _paymentService = paymentService;
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

        [HttpGet("best-selling-tour/{id}")]
        public ActionResult<TourDto> GetBestTourByAuthorId(int id)
        {
            var result = _authorDashboardService.GetBestSellingTourByAuthorId(id);
            return CreateResponse(result);
        }

        [HttpGet("least-selling-tour/{id}")]
        public ActionResult<TourDto> GetWorstTourByAuthorId(int id)
        {
            var result = _authorDashboardService.GetWorstSellingTourByAuthorId(id);
            return CreateResponse(result);
        }

        [HttpGet("lowest-rated-tour/{id}")]
        public ActionResult<TourDto> GetLowestRatedTourByAuthorId(int id)
        {
            var result = _authorDashboardService.GetLowestRatedTourByAuthorId(id);
            return CreateResponse(result);
        }

        [HttpGet("total-publishes/{id}")]
        public ActionResult<int> GetTotalPublishedTours(int id)
        {
            var result = _authorDashboardService.GetNumberOfPublishedTours(id);
            return CreateResponse(result);
        }

        [HttpGet("total-purchased/{id}")]
        public ActionResult<int> GetTotalPurchasedTours(int id)
        {
            var result = _authorDashboardService.GetNumberOfPurchashedTours(id);
            return CreateResponse(result);
        }
        [HttpGet("total-sales/{id}")]
        public ActionResult<int> GetTotalSales(int id)
        {
            var result = _authorDashboardService.GetTotalSales(id);
            return CreateResponse(result);
        }


    }
}
