using Explorer.Payments.API.Dtos.Payments;
using Explorer.Payments.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Shopping
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/shopping/payment")]
    public class PaymentController:BaseApiController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public ActionResult<PaymentDto> Create()
        {
            if (int.TryParse(User.FindFirst("id")?.Value, out int touristId))
            {
                var result = _paymentService.Create(touristId);
                return CreateResponse(result);
            }
            else
            {
                return BadRequest("Invalid input");
            }
        }
    }
}
