using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;

namespace Explorer.Tours.Core.UseCases
{
    public class EquipmentManagementService : CrudService<EquipmentManagementDto, EquipmentManagement>, IEquipmentManagementService
    {

        private readonly List<EquipmentManagementDto> equipmentStorage;
        private readonly IEquipmentManagementRepository _equipmentManagementRepository;
        private readonly ICrudRepository<EquipmentManagement> _crudRepository;
        private readonly IMapper _mapper;


        public EquipmentManagementService(ICrudRepository<EquipmentManagement> crudRepository, IMapper mapper, IEquipmentManagementRepository equipmentManagementRepository) : base(crudRepository, mapper)
        {
            _crudRepository = crudRepository;
            _mapper = mapper;
            _equipmentManagementRepository = equipmentManagementRepository;
        }


        public List<EquipmentManagementDto> GetEquipmentList()
        {

            var equipment = _equipmentManagementRepository.GetAll();
            var equipmentManagementDtos = equipment.Select(e => _mapper.Map<EquipmentManagementDto>(e)).ToList();
            return equipmentManagementDtos;

        }

        public Result<List<EquipmentManagementDto>> GetAllEquipment()
        {
            return equipmentStorage;
        }
        public List<EquipmentManagementDto> GetEquipmentListForTourist(int touristId)
        {

            return equipmentStorage.FindAll(e => e.TouristId == touristId);
        }

        //public Result AddEquipment(EquipmentManagementDto equipmentDto)
        //{

        //    if (!equipmentStorage.Exists(e => e.TouristId == equipmentDto.TouristId && e.EquipmentId == equipmentDto.EquipmentId))
        //    {
        //        equipmentDto.Status = Explorer.Tours.API.Dtos.Status.Added;
        //        equipmentStorage.Add(equipmentDto);
        //        return 
        //    }
        //    else
        //        return null;
        //}

        //public EquipmentManagementDto RemoveEquipment(EquipmentManagementDto equipmentDto)
        //{

        //    var equipmentToRemove = equipmentStorage.Find(e => e.TouristId == equipmentDto.TouristId && e.EquipmentId == equipmentDto.EquipmentId);
        //    if (equipmentToRemove != null)
        //    {
        //        equipmentToRemove.Status = Explorer.Tours.API.Dtos.Status.Removed;
        //        equipmentStorage.Remove(equipmentToRemove);
        //        return equipmentToRemove;
        //    }
        //    else
        //        return null;
        //}

        public Result<EquipmentManagementDto> GetEquipmentByUser(int id)
        {
            var equipment = GetEquipmentList();
            var eq = equipment.FirstOrDefault(e => e.TouristId.Equals(id));

            if (eq != null)
            {
                return Result.Ok(eq);
            }
            else
                return Result.Fail("Equipment for user not found");
        }
    }
}