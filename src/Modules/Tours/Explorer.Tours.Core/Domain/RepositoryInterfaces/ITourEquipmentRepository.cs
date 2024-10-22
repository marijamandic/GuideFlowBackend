using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ITourEquipmentRepository
    {
        

        List<TourEquipment> GetByTour(int tourId);
        List<Equipment> GetEquipmentByTour(int tourId);

        List<TourEquipment> GetAll();
    }
}
