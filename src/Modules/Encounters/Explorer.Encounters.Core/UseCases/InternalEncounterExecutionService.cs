using AutoMapper;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Internal;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.UseCases
{
    public class InternalEncounterExecutionService : IInternalEncounterExecutionService
    {
        private readonly IEncounterExecutionRepository _encounterExecutionRepository;
        public InternalEncounterExecutionService(IEncounterExecutionRepository encounterExecutionRepository)
        {
            _encounterExecutionRepository = encounterExecutionRepository;
        }
        public Result<bool> IsEncounterExecutionFinished(long userId, long encounterId)
        {
            var allExecutions = _encounterExecutionRepository.GetAll();
            var execution = allExecutions.FirstOrDefault(e => e.UserId == userId && e.EncounterId == encounterId);
            if (execution == null) return Result.Fail("EncounterExecution not found");
            return execution.ExecutionStatus == ExecutionStatus.Completed;
        }
    }
}
