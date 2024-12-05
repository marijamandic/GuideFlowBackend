using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces
{
    public interface IEncounterExecutionRepository
    {
        List<EncounterExecution> GetAll();
        EncounterExecution GetByUserId(long userId);
        List<long> GetAllEncounterIdsByUserId(long userId);
        List<EncounterExecution> GetByEncounterId(long encounterId);
        PagedResult<EncounterExecution> GetPaged(int page, int pageSize);
        EncounterExecution Get(long id);
        EncounterExecution Create(EncounterExecution entity);
        Task<EncounterExecution> Update(EncounterExecution encounterExecution);
        void Delete(long id);
    }
}
