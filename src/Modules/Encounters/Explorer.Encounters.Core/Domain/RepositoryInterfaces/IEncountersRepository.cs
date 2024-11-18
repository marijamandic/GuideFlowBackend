using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces
{
    public interface IEncountersRepository : ICrudRepository<Encounter>
    {
        public SocialEncounter GetSocial(long id);
        public LocationEncounter GetLocation(long id);
        public MiscEncounter GetMisc(long id);
    }
}
