using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public
{
    public interface IEquipmentManagementService
    {
        Result<PagedResult<EquipmentManagementDto>> GetPaged(int page, int pageSize);
        Result<EquipmentManagementDto> Create(EquipmentManagementDto tourEquipment);
        Result<EquipmentManagementDto> Update(EquipmentManagementDto tourEquipment);
        Result Delete(int id);
        Result<EquipmentManagementDto> GetEquipmentByUser(int id);
        public Result<EquipmentManagementDto> DeleteEquipmentById(int equipmentId);
    }
}
