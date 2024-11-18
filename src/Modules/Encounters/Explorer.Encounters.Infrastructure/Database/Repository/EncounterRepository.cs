using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Infrastructure.Database.Repository
{
    public class EncounterRepository : CrudDatabaseRepository<Encounter,EncountersContext>, IEncountersRepository
    {
        private readonly EncountersContext _context;
        public EncounterRepository(EncountersContext context) : base(context) { 
            _context = context;
        }
        public SocialEncounter GetSocial(long id)
        {
            var encounter = _context.Encounters.Find(id);
            if (encounter is SocialEncounter socialEncounter)
            {
                return socialEncounter;
            }
            return null;
        }
        public MiscEncounter GetMisc(long id)
        {
            var encounter = _context.Encounters.Find(id);
            if (encounter is MiscEncounter miscEncounter)
            {
                return miscEncounter;
            }
            return null;
        }
        public LocationEncounter GetLocation(long id)
        {
            var encounter = _context.Encounters.Find(id);
            if (encounter is LocationEncounter locationEncounter)
            {
                return locationEncounter;
            }
            return null;
        }
    }
}
