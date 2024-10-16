using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    //Sta znaci naslediti CRUD ili BASE Service??
    public class RatingAppService : CrudService<RatingAppDto, AppRating>, IRatingAppService
    {
        public RatingAppService(ICrudRepository<AppRating> repository, IMapper mapper) : base(repository, mapper) 
        {
            
        }
        public Result<PagedResult<RatingAppDto>> GetPaged(int page, int pageSize)
        {
            return base.GetPaged(page, pageSize); 
        }
    }
}
