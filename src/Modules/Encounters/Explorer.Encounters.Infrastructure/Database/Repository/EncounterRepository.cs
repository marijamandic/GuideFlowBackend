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
        public PagedResult<Encounter> SearchAndFilter(string? name, int? type, double? latitude, double? longitude)
        {
            IQueryable<Encounter> query = _context.Encounters;

            if (!string.IsNullOrWhiteSpace(name))
            {
                string normalizedName = name.ToLower();
                query = query.Where(e => EF.Functions.Like(e.Name.ToLower(), $"%{normalizedName}%"));
            }

            if (type.HasValue && Enum.IsDefined(typeof(EncounterType), type.Value))
            {
                var encounterType = (EncounterType)type.Value;
                query = query.Where(e => e.EncounterType == encounterType);
            }

            // Filtriranje po lokaciji u krugu od 100m
            //Ne pitaj zasto se primenjuje ova formula al nasao sam negde na internetu
            if ((latitude.HasValue && longitude.HasValue) && (latitude != 0 && longitude != 0))
            {
                const double earthRadiusKm = 6371; // Zemljin poluprecnik u kilometrima
                const double maxDistanceKm = 1; // Maksimalna udaljenost u kilometrima (100m)

                double lat = latitude.Value;
                double lon = longitude.Value;

                query = query.Where(e =>
                    earthRadiusKm * 2 * Math.Asin(Math.Sqrt(
                        Math.Pow(Math.Sin((e.EncounterLocation.Latitude - lat) * Math.PI / 180 / 2), 2) +
                        Math.Cos(lat * Math.PI / 180) *
                        Math.Cos(e.EncounterLocation.Latitude * Math.PI / 180) *
                        Math.Pow(Math.Sin((e.EncounterLocation.Longitude - lon) * Math.PI / 180 / 2), 2)
                    )) <= maxDistanceKm);
            }

            var results = query.ToList();
            return new PagedResult<Encounter>(results, results.Count);
        }
    }
}
