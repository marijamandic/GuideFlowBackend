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

    }
}
