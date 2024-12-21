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
            public Result<List<UserDto>> GetAllUsers();
            Result Delete(int id);
            Result<TouristDto> GetTouristById(int id);
            Result<TouristDto> AddTouristXp(int id, int amount);
            Result<TouristDto> TakeTouristAdventureCoins(int touristId, int amount);

            Result<TouristDto> AddTouristMoney(int id, int amount);
        //Result<TouristDto> CreateTourist(UserDto userDto);
            //Result<TouristDto> CreateTourist(UserDto userDto);
            Result<PagedResult<TouristDto>> GetTouristsPaged(int page, int pageSize);
            Result<AuthorDto> GetAuthorById(int id);

            Result<AuthorDto> AddAuthorMoney(int id, int amount);
        }
}
