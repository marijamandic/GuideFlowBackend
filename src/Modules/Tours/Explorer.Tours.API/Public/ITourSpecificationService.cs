﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public
{
    public interface ITourSpecificationService
    {
        Result<PagedResult<TourSpecificationDto>> GetPaged(int page, int pageSize);
        Result<TourSpecificationDto> Create(TourSpecificationDto tourSpecificationDto);
        Result<TourSpecificationDto> Update(TourSpecificationDto tourSpecificationDto);
        Result Delete(int id);
        Result<TourSpecificationDto> GetTourSpecificationsByUserId(long userId);
        Task<bool> HasPreferenceAsync(int userId);

    }
}
