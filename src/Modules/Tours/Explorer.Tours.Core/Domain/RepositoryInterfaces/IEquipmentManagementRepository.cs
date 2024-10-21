using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IEquipmentManagementRepository
    {
        EquipmentManagement? GetEquipmentById(int id);
        List<EquipmentManagement> GetByTouristId(int touristId);
        List<EquipmentManagement> GetByStatus(Status status);
        EquipmentManagement Add(EquipmentManagement equipmentManagement);
        void Remove(EquipmentManagement equipmentManagement);

        IEnumerable<EquipmentManagement> GetAll();

    }
}
