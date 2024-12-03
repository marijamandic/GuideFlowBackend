using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Encounters.API.Internal;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Encounters.Core.Mappers;
using Explorer.Encounters.Core.UseCases;
using Explorer.Encounters.Infrastructure.Database;
using Explorer.Encounters.Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Infrastructure
{
    public static class EncountersStartup
    {
        public static IServiceCollection ConfigureEncountersModule(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(EncounterProfile).Assembly);
            SetupCore(services);
            SetupInfrastructure(services);
            return services;
        }

        private static void SetupCore(IServiceCollection services)
        {
            services.AddScoped<IEncounterService,EncounterService>();
            services.AddScoped<IEncounterExecutionService, EncounterExecutionService>();
            services.AddScoped<IInternalEncounterExecutionService, InternalEncounterExecutionService>();
        }
        private static void SetupInfrastructure(IServiceCollection services)
        {
            services.AddScoped<IEncountersRepository,EncounterRepository>();
            services.AddScoped(typeof(ICrudRepository<Encounter>), typeof(CrudDatabaseRepository<Encounter, EncountersContext>));
            services.AddScoped<IEncounterExecutionRepository,EncounterExecutionRepository>();

            services.AddDbContext<EncountersContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("encounters"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "encounters")));
        }
    }
}
