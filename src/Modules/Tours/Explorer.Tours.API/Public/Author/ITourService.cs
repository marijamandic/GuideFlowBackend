﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Author
{
    public interface ITourService
    {
        Result<PagedResult<TourDto>> GetPaged(int page, int pageSize);
        Result<TourDto> Create(TourDto tour);
        Result<TourDto> Update(TourDto tour);
        Result Delete(int id);
        Result<TourDto> Get(int id);
    }
}
