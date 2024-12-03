using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Internal
{
    public interface IInternalEncounterExecutionService
    {
        Result<bool> IsEncounterExecutionFinished(long userId, long encoutnerId);
    }
}
