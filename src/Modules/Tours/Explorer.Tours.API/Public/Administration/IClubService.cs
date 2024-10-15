﻿using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Administration
{
    public interface IClubService
    {
        Result<ClubDto> Create(ClubDto club);
        Result<ClubDto> Update(ClubDto club);
        Result Delete(int id);
    }
}
