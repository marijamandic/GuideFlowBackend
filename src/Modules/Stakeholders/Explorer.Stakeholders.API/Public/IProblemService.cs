using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos.Problems;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public;
public interface IProblemService
{
    Result<ProblemDto> Create(ProblemDto problem);

    /// <summary>
    /// Gets all problems with all value objects and related entitites.
    /// Using .Include method from EF
    /// </summary>
    /// <returns></returns>
    Result<PagedResult<ProblemDto>> GetAll();
    Result<ProblemDto> Update(ProbStatusChangeDto status,int id);
    Result<ProblemDto> UpdateDeadline(int id, DateTime deadline);
}
