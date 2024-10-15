using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
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
    public class TourEquipmentRepository : ITourEquipmentRepository
    {
        private readonly ToursContext _context;

        public TourEquipmentRepository(ToursContext context)
        {
            _context = context;
        }

        public List<TourEquipment> GetByTour(int tourId)
        {
            return _context.TourEquipment
                .Where(te => te.TourId == tourId)
                .ToList(); // No need to include related entities since there's no navigation property
        }

        public List<Equipment> GetEquipmentByTour(int tourId)
        {
            var tourEquipments = _context.TourEquipment
                .Where(te => te.TourId == tourId)
                .ToList();

            var equipmentIds = tourEquipments.Select(te => te.EquipmentId).ToList();

            return _context.Equipment
                .Where(e => equipmentIds.Contains((int)e.Id))
            .ToList();
        }

        //za nasledjivanje ICrudRepository

       /* public PagedResult<TourEquipment> GetPaged(int page, int pageSize)
        {
            var task = _dbSet.GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }

        public TourEquipment Get(long id)
        {
            var entity = _dbSet.Find(id);
            if (entity == null) throw new KeyNotFoundException("Not found: " + id);
            return entity;
        }

        public TourEquipment Create(TourEquipment entity)
        {
            _dbSet.Add(entity);
            DbContext.SaveChanges();
            return entity;
        }

        public TourEquipment Update(TourEquipment entity)
        {
            try
            {
                DbContext.Update(entity);
                DbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return entity;
        }

        public void Delete(long id)
        {
            var entity = Get(id);
            _dbSet.Remove(entity);
            DbContext.SaveChanges();
        }*/

    }
}
