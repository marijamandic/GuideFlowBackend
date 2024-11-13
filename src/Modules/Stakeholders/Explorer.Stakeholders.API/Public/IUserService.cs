﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{

        public interface IUserService
        {
            Result<PagedResult<UserDto>> GetPaged(int page, int pageSize);
            Result<UserDto> GetById(int id);
            Result<UserDto> Create(UserDto userDto);  
            Result<UserDto> Update(UserDto userDto);  
            Result Delete(int id);                    
        }


}
