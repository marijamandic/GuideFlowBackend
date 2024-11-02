using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Public.Administration;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class PublicPointService : CrudService<PublicPointDto, PublicPoint>, IPublicPointService
    {
        private readonly IPublicPointRepository _publicPointRepository;
        private readonly IMapper _mapper; 

        public PublicPointService(ICrudRepository<PublicPoint> repository, IMapper mapper, IPublicPointRepository publicPointRepository)
            : base(repository, mapper)
        {
            _publicPointRepository = publicPointRepository;
            _mapper = mapper;
        }
    }
}
