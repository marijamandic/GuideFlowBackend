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
            var task = DbContext.Tours.Where(t=> t.Status != TourStatus.Deleted).Include(t=>t.Checkpoints).Include(t=>t.Reviews).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }

        public new Tour Get(long id)
        {
            var entity = DbContext.Tours.Where(t => t.Id == id && t.Status != TourStatus.Deleted)
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

        public new void Delete(long id)
        {
            var entity = Get(id);
            DbContext.Tours.Remove(entity);
            DbContext.SaveChanges();
        }

        public PagedResult<Tour> GetByAuthorId(int authorId)
        {
            var result = DbContext.Tours.Where(t => t.AuthorId == authorId).ToList();
            return new PagedResult<Tour>(result, result.Count);
        }

        public List<long> GetListByAuthorId(int authorId)
        {
            return DbContext.Tours
                            .Where(t => t.AuthorId == authorId)
                            .Select(t => t.Id) 
                            .ToList();
        }
    }
}
