using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.TourExecutions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourExecutionRepository : CrudDatabaseRepository<TourExecution,ToursContext> ,ITourExecutionRepository
    {
        private readonly ToursContext _context;

        public TourExecutionRepository(ToursContext context) : base(context) 
        {
            _context = context;
        }

        public new TourExecution Get(long id)
        {
            return _context.TourExecutions.Where(te => te.Id == id).Include(te => te.CheckpointsStatus).FirstOrDefault() ?? throw new Exception("Id not found");
        }
        public new TourExecution Update(TourExecution tourExecution) { 
            _context.Entry(tourExecution).State = EntityState.Modified;
            _context.SaveChanges();
            return tourExecution;
        }

        public new TourExecution GetByUserId(long userId)
        {
            return _context.TourExecutions
                           .FirstOrDefault(te => te.UserId == userId);
        }
    }
}
