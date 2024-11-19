﻿using Explorer.Tours.Core.Domain.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ICheckpointRepository
    {
        List<Checkpoint> GetByTour(int id);
    }
}
