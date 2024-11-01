using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourDatabaseRepository:CrudDatabaseRepository<Tour,ToursContext>,ITourRepository
    {
        public TourDatabaseRepository(ToursContext dbContext):base(dbContext) { }

        public new PagedResult<Tour> GetPaged(int page, int pageSize)
        {
            var task = DbContext.Tours.Include(t=>t.Checkpoints).Include(t=>t.Reviews).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }

        public new Tour Get(long id)
        {
            var entity = DbContext.Tours.Where(t => t.Id == id)
            .Include(t => t.Checkpoints)
            .Include(t => t.Reviews)
            .FirstOrDefault();
            if (entity == null) throw new KeyNotFoundException("Not found: " + id);
            return entity;
        }

        public new Tour Update(Tour tour)
        {
            DbContext.Entry(tour).State = EntityState.Modified;
            DbContext.SaveChanges();
            return tour;
        }
    }
}
