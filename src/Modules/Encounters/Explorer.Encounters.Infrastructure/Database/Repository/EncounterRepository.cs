using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Infrastructure.Database.Repository
{
    public class EncounterRepository : IEncountersRepository
    {
        private readonly EncountersContext _context;
        public EncounterRepository(EncountersContext context) { 
            _context = context;
        }
        public PagedResult<Encounter> GetPaged(int page, int pageSize)
        {
            var task = _context.Encounters.GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }
        public Encounter Get(long id)
        {
            var encounter = _context.Encounters.FirstOrDefault(e => e.Id == id);

            if (encounter is SocialEncounter socialEncounter)
                return socialEncounter;
            else if (encounter is MiscEncounter miscEncounter)
                return miscEncounter;
            else if (encounter is HiddenLocationEncounter hiddenLocationEncounter)
                return hiddenLocationEncounter;

            return encounter;
        }
        public Encounter Create(Encounter encounter)
        {
            _context.Encounters.Add(encounter);
            _context.SaveChanges();
            return encounter;
        }

        public virtual Encounter Update(Encounter encounter)
        {
            try
            {
                _context.Update(encounter);
                _context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return encounter;
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }
        public PagedResult<Encounter> SearchAndFilter(string? name, int? type)
        {
            IQueryable<Encounter> query = _context.Encounters;

            if (!string.IsNullOrWhiteSpace(name))
            {
                string normalizedName = name.ToLower();
                query = query.Where(e => EF.Functions.Like(e.Name.ToLower(), $"%{normalizedName}%"));
            }

            if (type.HasValue)
            {
                // Pretvaranje int vrednosti u EncounterType ako je validno
                if (Enum.IsDefined(typeof(EncounterType), type.Value))
                {
                    var encounterType = (EncounterType)type.Value;
                    query = query.Where(e => e.EncounterType == encounterType);
                }
            }

            var results = query.ToList();
            return new PagedResult<Encounter>(results, results.Count);
        }
    }
}
