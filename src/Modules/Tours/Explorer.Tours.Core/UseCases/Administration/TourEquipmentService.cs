using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class TourEquipmentService : CrudService<TourEquipmentDto, TourEquipment>, ITourEquipmentService
    {

        private readonly ITourEquipmentRepository _tourEquipmentRepository;
        private readonly IMapper _mapper; // Injektovanje mappera

        public TourEquipmentService(ICrudRepository<TourEquipment> repository, IMapper mapper, ITourEquipmentRepository tourEquipmentRepository) : base(repository, mapper)
        {
            _tourEquipmentRepository = tourEquipmentRepository;
            _mapper = mapper;
        }
        public List<TourEquipmentDto> GetByTour(int tourId)
        {
            var tourEquipments = _tourEquipmentRepository.GetByTour(tourId);
            return _mapper.Map<List<TourEquipmentDto>>(tourEquipments); // Mapiranje
        }

        public List<EquipmentDto> GetEquipmentByTour(int tourId)
        {
            var equipments = _tourEquipmentRepository.GetEquipmentByTour(tourId);
             return _mapper.Map<List<EquipmentDto>>(equipments); // Mapiranje
        
        }

        public List<TourEquipmentDto> GetAll()
        {
            var result = _tourEquipmentRepository.GetAll();
            return _mapper.Map<List<TourEquipmentDto>>(result); // Mapiranje

        }



    }
}
