using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Internal
{
    public interface IInternalTouristService
    {
        Result<TouristDto> AddTouristXp(int id, int amount);
        //Result<TouristDto> UpdateTourist(TouristDto touristDto);
    }
}
