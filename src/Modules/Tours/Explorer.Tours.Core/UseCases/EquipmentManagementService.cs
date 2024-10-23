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

        
        public IEnumerable<EquipmentManagementDto> GetAllEquipment_()
        {
            var eqMan = _equipmentManagementRepository.GetAll();
            var eqManDtos = eqMan.Select(e => _mapper.Map<EquipmentManagementDto>(e));
            return eqManDtos;
        }

        public Result<EquipmentManagementDto> GetEquipmentByUser(int id)
        {
            var equipment = GetAllEquipment_();
            //foreach(EquipmentManagementDto equ in equipment)
            //{
            //    if(equ.TouristId == id)
            //        return Result.Ok(equ);
            //}
            var eq = equipment.FirstOrDefault(e => e.TouristId == id);

            if (eq != null)
            {
                return Result.Ok(eq);
            }
            else
                return Result.Fail("Equipment for user not found");
        }


        public Result<EquipmentManagementDto> DeleteEquipmentById(int equipmentId)
        {
            var equipment = _equipmentManagementRepository.GetEquipmentById(equipmentId);
            if (equipment == null)
            {
                return Result.Fail(FailureCode.NotFound); 
            }

            _equipmentManagementRepository.Remove(equipment);
            return Result.Ok();
        }

    }
}