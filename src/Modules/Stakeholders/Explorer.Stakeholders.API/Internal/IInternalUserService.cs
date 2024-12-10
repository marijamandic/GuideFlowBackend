using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Internal
{
    public interface IInternalUserService
    {
        Result<string> GetUsername(long id);
        Result<Dictionary<long, string>> GetUsernamesByIds(List<long> ids);
    }
}
