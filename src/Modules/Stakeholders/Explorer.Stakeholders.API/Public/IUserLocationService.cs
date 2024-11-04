using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IUserLocationService
    {
        Result<UserLocationDto> GetById(int id);

        Result<UserLocationDto> UpdateLocation(UserLocationDto userLocation);

        Result<UserLocationDto> AddUserLocation(UserLocationDto userLocation);
    }
}
