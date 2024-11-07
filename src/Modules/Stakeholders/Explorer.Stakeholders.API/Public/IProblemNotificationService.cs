﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public;

public interface IProblemNotificationService
{
    Result Create(CreateProblemNotificationInputDto notificationInput);
    Result<PagedResult<ProblemNotificationDto>> GetByUserId(int userId);
}
