using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Internal
{
    public interface IInternalUserService
    {
        Result<string> GetUsername(long id);
        Result<Dictionary<long, string>> GetUsernamesByIds(List<long> ids);
        public Result<TouristDto> GetTouristById(int id);
        public Result<TouristDto> TakeTouristAdventureCoins(int touristId, int amount);
    }
}
