using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Author;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.TourExecutions;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Core.Mappers;
using Explorer.Tours.Core.UseCases;
using Explorer.Tours.Core.UseCases.Administration;
using Explorer.Tours.Core.UseCases.Authoring;
using Explorer.Tours.Core.UseCases.Execution;
using Explorer.Tours.Infrastructure.Database;
using Explorer.Tours.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Explorer.Tours.Infrastructure;

public static class ToursStartup
{
    public static IServiceCollection ConfigureToursModule(this IServiceCollection services)
    {
        // Registers all profiles since it works on the assembly
        services.AddAutoMapper(typeof(ToursProfile).Assembly);
        SetupCore(services);
        SetupInfrastructure(services);
        return services;
    }
    
    private static void SetupCore(IServiceCollection services)
    {
        services.AddScoped<IEquipmentService, EquipmentService>();
        services.AddScoped<ITourService, TourService>();
        services.AddScoped<ITourEquipmentService, TourEquipmentService>();

        services.AddScoped<ICheckpointService, CheckpointService>();
        services.AddScoped<ITourObjectService, TourObjectService>();
        services.AddScoped<IEquipmentManagementService, EquipmentManagementService>();
        services.AddScoped<IEquipmentManagementRepository, EquipmentManagementRepository>();
        services.AddScoped<ITourReviewService, TourReviewService>();
        services.AddScoped<ITourSpecificationService, TourSpecificationService>();
        services.AddScoped<IPublicPointService, PublicPointService>();
        services.AddScoped<ITourExecutionService, TourExecutionService>();

    }

    private static void SetupInfrastructure(IServiceCollection services)
    {
        services.AddScoped<ITourRepository,TourDatabaseRepository>();
        services.AddScoped(typeof(ICrudRepository<Equipment>), typeof(CrudDatabaseRepository<Equipment, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<TourEquipment>), typeof(CrudDatabaseRepository<TourEquipment, ToursContext>));
        services.AddScoped<ITourEquipmentRepository, TourEquipmentRepository>();
        services.AddScoped(typeof(ICrudRepository<Checkpoint>), typeof(CrudDatabaseRepository<Checkpoint, ToursContext>));
        services.AddScoped<ICheckpointRepository, CheckpointRepository>();
        services.AddScoped(typeof(ICrudRepository<TourObject>), typeof(CrudDatabaseRepository<TourObject, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<EquipmentManagement>), typeof(CrudDatabaseRepository<EquipmentManagement, ToursContext>));
        services.AddScoped<IEquipmentManagementRepository, EquipmentManagementRepository>();
        services.AddScoped(typeof(ICrudRepository<TourReview>), typeof(CrudDatabaseRepository<TourReview, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<TourSpecifications>), typeof(CrudDatabaseRepository<TourSpecifications, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<PublicPoint>), typeof(CrudDatabaseRepository<PublicPoint, ToursContext>));

        services.AddScoped<ITourExecutionRepository, TourExecutionRepository>();
        services.AddScoped(typeof(ICrudRepository<TourExecution>), typeof(CrudDatabaseRepository<TourExecution, ToursContext>));
        services.AddScoped<ITourSpecificationRepository, TourSpecificationRepository>();
        services.AddScoped<IPublicPointRepository, PublicPointRepository>();

        services.AddDbContext<ToursContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("tours"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "tours")));
    }
}