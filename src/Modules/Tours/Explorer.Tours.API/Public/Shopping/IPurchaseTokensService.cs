using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.Shopping;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Shopping
{
    public interface IPurchaseTokensService
    {
        Result<PagedResult<PurchaseTokenDto>> GetPaged(int page, int pageSize);
        Result<PurchaseTokenDto> Get(int id);
        Result<PurchaseTokenDto> Create(PurchaseTokenDto purchaseToken);
        Result<PurchaseTokenDto> Update(PurchaseTokenDto purchaseToken);
        Result Delete(int id); Result<PagedResult<PurchaseTokenDto>> GetTokensByUserId(int userId);
    }
}
