using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Internal
{
    public interface IInternalTourBundleService
    {
        Result<List<int>> GetToursById(int bundleId);
    }
}
