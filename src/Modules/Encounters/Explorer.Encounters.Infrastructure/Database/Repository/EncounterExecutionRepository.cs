
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Infrastructure.Database.Repository
{
    public class EncounterExecutionRepository : IEncounterExecutionRepository
    {
        private readonly EncountersContext _context;
        public EncounterExecutionRepository(EncountersContext context)
        {
            _context = context;
        }

        public EncounterExecution Create(EncounterExecution encounterExecution)
        {
            _context.EncounterExecutions.Add(encounterExecution);
            _context.SaveChanges();
            return encounterExecution;
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public EncounterExecution Get(long id)
        {
            var encounterExecution = _context.EncounterExecutions.FirstOrDefault(e => e.Id == id);
            if(encounterExecution.Id == null)
                throw new KeyNotFoundException(nameof(id));
            return encounterExecution;
        }

        public PagedResult<EncounterExecution> GetPaged(int page, int pageSize)
        {
            var task = _context.EncounterExecutions.GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }

        public EncounterExecution Update(EncounterExecution encounterExecution)
        {
            try
            {
                _context.Update(encounterExecution);
                _context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return encounterExecution;
        }

        public List<EncounterExecution> GetAll()
        {
            return _context.EncounterExecutions.ToList();
        }
    }
}
