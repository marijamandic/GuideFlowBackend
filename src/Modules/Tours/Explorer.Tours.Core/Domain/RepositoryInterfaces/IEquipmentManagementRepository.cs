using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IEquipmentManagementRepository
    {
        EquipmentManagement GetById(int id);
        List<EquipmentManagement> GetByTouristId(int touristId);
        List<EquipmentManagement> GetByStatus(Status status);
        void Add(EquipmentManagement equipmentManagement);
        void Remove(EquipmentManagement equipmentManagement);
    }
}
