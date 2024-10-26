using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos.Club;
using Explorer.Stakeholders.API.Public.Club;
using Explorer.Stakeholders.Core.Domain.Club;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases.Club
{
    public class ClubService : CrudService<ClubDto, Explorer.Stakeholders.Core.Domain.Club.Club>, IClubService
    {
        public ClubService(ICrudRepository<Explorer.Stakeholders.Core.Domain.Club.Club> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {
        }
    }
}
