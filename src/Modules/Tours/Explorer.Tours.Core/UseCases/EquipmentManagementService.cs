using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases
{
    public class EquipmentManagementService : CrudService<EquipmentManagementDto, EquipmentManagement>, IEquipmentManagementService
    {
        // Simuliramo skladište opreme
        private readonly List<EquipmentManagementDto> equipmentStorage = new List<EquipmentManagementDto>();

        public EquipmentManagementService(ICrudRepository<EquipmentManagement> repository, IMapper mapper) : base(repository, mapper) { }

        public List<EquipmentManagementDto> GetEquipmentList()
        {
            // Vrati listu opreme 
            return equipmentStorage;
        }
        public List<EquipmentManagementDto> GetEquipmentListForTourist(int touristId)
        {
            // Vrati listu opreme za određenog turistu
            return equipmentStorage.FindAll(e => e.TouristId == touristId);
        }

        public EquipmentManagementDto AddEquipment(EquipmentManagementDto equipmentDto)
        {
            // Dodaj opremu u skladište
            if (!equipmentStorage.Exists(e => e.TouristId == equipmentDto.TouristId && e.EquipmentId == equipmentDto.EquipmentId))
            {
                equipmentDto.Status = Explorer.Tours.API.Dtos.Status.Added;
                equipmentStorage.Add(equipmentDto);
                return equipmentDto;
            }
            else
                return null;
        }

        public EquipmentManagementDto RemoveEquipment(EquipmentManagementDto equipmentDto)
        {
            // Ukloni opremu iz skladišta
            var equipmentToRemove = equipmentStorage.Find(e => e.TouristId == equipmentDto.TouristId && e.EquipmentId == equipmentDto.EquipmentId);
            if (equipmentToRemove != null)
            {
                equipmentToRemove.Status = Explorer.Tours.API.Dtos.Status.Removed;
                equipmentStorage.Remove(equipmentToRemove);
                return equipmentToRemove;
            }
            else
                return null;
        }

    }
}