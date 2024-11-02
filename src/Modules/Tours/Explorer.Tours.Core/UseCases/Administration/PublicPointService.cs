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
using FluentResults;


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

        public Result<IEnumerable<PublicPointDto>> GetPendingPublicPoints()
        {
            try
            {
                var pendingPoints = _publicPointRepository.GetAll()
                    .Where(p => p.ApprovalStatus == 0)
                    .Select(p => new PublicPointDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Latitude = p.Latitude,
                        Longitude = p.Longitude,
                        ImageUrl = p.ImageUrl,
                        ApprovalStatus = (API.Dtos.ApprovalStatus)p.ApprovalStatus,
                        PointType = (API.Dtos.PointType)p.PointType
                    });

                return Result.Ok(pendingPoints);  
            }
            catch (Exception ex)
            {
                return Result.Fail(new Error("Failed to retrieve pending public points.")
                    .CausedBy(ex));
            }
        }
    }
}
