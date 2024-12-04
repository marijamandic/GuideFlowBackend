using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos.Payments;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Public
{
    public interface IPaymentService
    {
        Result<PaymentDto> Create(int touristId);
        Result<PagedResult<PaymentDto>> GetAllByTouristId(int touristId);
        public Result Checkout(int touristId);
    }
}
