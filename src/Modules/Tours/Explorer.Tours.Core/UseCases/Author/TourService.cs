using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Author;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Author
{
    public class TourService : CrudService<TourDto, Tour>, ITourService
    {
        private readonly ITourRepository tourRepository;
        public TourService(ITourRepository tourRepository, IMapper mapper) : base(tourRepository, mapper) 
        { 
            this.tourRepository=tourRepository;
        }
    }
}
