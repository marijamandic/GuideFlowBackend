﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos.Problems;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administrator/problems")]
    public class ProblemController : BaseApiController
    {
        private readonly IProblemService _problemService;

        public ProblemController(IProblemService problemService)
        {
            _problemService = problemService;
        }

        [HttpGet]
        public ActionResult<PagedResult<ProblemDto>> GetAll()
        {
            var result = _problemService.GetAll();
            return CreateResponse(result);
        }
        [HttpPut("{id:int}/deadline")]
        public ActionResult<ProblemDto> UpdateDeadline(int id, [FromBody] DeadlineDto deadline)
        {
            var result = _problemService.UpdateDeadline(id, deadline.Date);
            return CreateResponse(result);
        }
    }
}
