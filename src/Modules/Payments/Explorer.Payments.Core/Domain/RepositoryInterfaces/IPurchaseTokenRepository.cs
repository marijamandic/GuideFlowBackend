﻿using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces
{
    public interface IPurchaseTokenRepository
    {
        PurchaseToken Create(PurchaseToken entity);
        PagedResult<PurchaseToken> GetTourTokensByTouristId(long touristId);
    }
}
