using Explorer.Tours.API.Dtos.Shopping;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Internal
{
    public interface IInternalShoppingCartService
    {
        Result<ShoppingCartDto> Create(long userId);   
    }
}
