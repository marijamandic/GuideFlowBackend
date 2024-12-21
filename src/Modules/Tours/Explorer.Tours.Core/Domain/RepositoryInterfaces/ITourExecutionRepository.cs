﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Core.Domain.TourExecutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ITourExecutionRepository : ICrudRepository<TourExecution>
    {
        Task<TourExecution> GetTourExecutionByIdAsync(long tourExecutionId);
        TourExecution GetByUserId(long userId);
        List<long> GetCompletedToursByTourist(int userId);
    }
}
