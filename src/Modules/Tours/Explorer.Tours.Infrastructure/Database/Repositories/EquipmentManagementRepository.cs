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

        public EquipmentManagementRepository(ToursContext context)
        {
            _context = context;
        }

        public EquipmentManagement? GetEquipmentById(int id) 
        {
            return _context.EquipmentManagements.FirstOrDefault(e => e.Id == id);
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
        public EquipmentManagement Add(EquipmentManagement equipmentManagement) {
            _context.EquipmentManagements.Add(equipmentManagement);
            _context.SaveChanges();
            return equipmentManagement;
        }
        public void Remove(EquipmentManagement equipmentManagement) {
            //_context.EquipmentManagements.Remove(equipmentManagement);
            //_context.SaveChanges();

            var equipment = _context.EquipmentManagements.Find(equipmentManagement.Id);
            if (equipment != null)
            {
                _context.EquipmentManagements.Remove(equipment);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Equipment not found");
            }
        }

        public IEnumerable<EquipmentManagement> GetAll() 
        {  
            return _context.EquipmentManagements.ToList();
        }
    }
}
