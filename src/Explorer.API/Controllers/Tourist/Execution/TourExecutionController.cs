﻿using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.Execution;
using Explorer.Tours.API.Public.Execution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Execution
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/execution/tourExecution")]
    public class TourExecutionController : BaseApiController
    {
        private readonly ITourExecutionService _tourExecutionService;

        public TourExecutionController(ITourExecutionService tourExecutionService)
        {
            _tourExecutionService = tourExecutionService;
        }

        /*[HttpGet]
        public ActionResult<PagedResult<TourExecutionDto>> GetAll([FromQuery] int page , [FromQuery] int pageSize) {
            var result = _tourExecutionService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }*/

        [HttpGet("{id:int}")]
        public ActionResult<TourExecutionDto> GetById(int id)
        {
            var result = _tourExecutionService.Get(id);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<TourExecutionDto> Create([FromBody] CreateTourExecutionDto createTourExecutionDto) {
            var result = _tourExecutionService.Create(createTourExecutionDto);
            return CreateResponse(result);
        }
        [HttpPut]
        public ActionResult<TourExecutionDto> Update([FromBody] UpdateTourExecutionDto updateTourExecutionDto) {
            var result = _tourExecutionService.Update(updateTourExecutionDto);
            return CreateResponse(result);
        }

        [HttpGet("getByUser/{userId:long}")]
        public ActionResult<TourExecutionDto> GetByUser(long userId)
        {
            var result = _tourExecutionService.GetSessionsByUserId(userId);
            return CreateResponse(result);
        }

        // za swagger

        [HttpPut("complete/{userId}")]
        public ActionResult<TourExecutionDto> CompleteSession(long userId)
        {
            var result = _tourExecutionService.CompleteSession(userId);
            return CreateResponse(result);
        }

        [HttpPut("abandon/{userId}")]
        public ActionResult<TourExecutionDto> AbandonSession(long userId)
        {
            var result = _tourExecutionService.AbandonSession(userId);
            return CreateResponse(result);
        }
    }
}
