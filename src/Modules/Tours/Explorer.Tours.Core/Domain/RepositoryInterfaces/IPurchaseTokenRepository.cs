using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Core.Domain.Shopping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IPurchaseTokenRepository: ICrudRepository<PurchaseToken>
    {
        PagedResult<PurchaseToken> GetByUserId(int id);
    }
}
