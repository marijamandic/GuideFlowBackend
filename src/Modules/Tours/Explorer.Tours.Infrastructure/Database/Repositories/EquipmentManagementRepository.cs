using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class EquipmentManagementRepository : IEquipmentManagementRepository
    {
        private readonly ToursContext _context;
        public EquipmentManagementRepository() { }

        public EquipmentManagement GetById(int id) 
        {
            return _context.EquipmentManagements.Find(id);
        }

        public List<EquipmentManagement> GetByTouristId(int touristId) {
            return _context.EquipmentManagements
                    .Where(e => e.TouristId == touristId)
                    .ToList();
        }
        public List<EquipmentManagement> GetByStatus(Status status) {
            return _context.EquipmentManagements
                   .Where(e => e.Status == status)
                   .ToList();
        }
        public void Add(EquipmentManagement equipmentManagement) {
            _context.EquipmentManagements.Add(equipmentManagement);
            _context.SaveChanges();
        }
        public void Remove(EquipmentManagement equipmentManagement) {
            _context.EquipmentManagements.Remove(equipmentManagement);
            _context.SaveChanges();
        }
    }
}
