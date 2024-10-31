using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.API.Public.Club;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.Club;
using Explorer.Stakeholders.Core.Domain.Problems;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces.Club;
using Explorer.Stakeholders.Core.Mappers;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Stakeholders.Core.UseCases.Club;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Stakeholders.Infrastructure.Database.Repositories;
using Explorer.Stakeholders.Infrastructure.Database.Repositories.Club;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Explorer.Stakeholders.Infrastructure;

public static class StakeholdersStartup
{
    public static IServiceCollection ConfigureStakeholdersModule(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(StakeholderProfile).Assembly);
        SetupCore(services);
        SetupInfrastructure(services);
        return services;
    }

    private static void SetupCore(IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>(); //Ova linija kaze kada god ti treba IAuthenticationService, koristi konkretnu implementaciju AuthenticationService
        services.AddScoped<ITokenGenerator, JwtGenerator>();
        services.AddScoped<IProfileInfoService, ProfileInfoService>(); // From HEAD
        services.AddScoped<IClubService, ClubService>(); // From the other branch
        services.AddScoped<IClubInvitationService, ClubInvitationService>();
        services.AddScoped<IClubRequestService, ClubRequestService>();
        services.AddScoped<IClubMemberService, ClubMemberService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IProblemService, ProblemService>();
        services.AddScoped<IRatingAppService, RatingAppService>();
        services.AddScoped<IAccountService, AccountService>();
    }

    private static void SetupInfrastructure(IServiceCollection services)
    {
        services.AddScoped(typeof(ICrudRepository<Person>), typeof(CrudDatabaseRepository<Person, StakeholdersContext>));      
        services.AddScoped<IUserRepository, UserDatabaseRepository>();
        services.AddScoped(typeof(ICrudRepository<ProfileInfo>), typeof(CrudDatabaseRepository<ProfileInfo, StakeholdersContext>)); // From HEAD
        services.AddScoped(typeof(ICrudRepository<Club>), typeof(CrudDatabaseRepository<Club, StakeholdersContext>)); // From the other branch
        services.AddScoped<IClubInvitationRepository, ClubInvitationDatabaseRepository>();
        services.AddScoped<IClubRequestRepository, ClubRequestDatabaseRepository>();
        services.AddScoped<IClubMemberRepository, ClubMemberDatabaseRepository>();
        services.AddScoped(typeof(ICrudRepository<Problem>), typeof(CrudDatabaseRepository<Problem, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<AppRating>), typeof(CrudDatabaseRepository<AppRating, StakeholdersContext>));
        services.AddScoped<IProblemRepository, ProblemDatabaseRepository>();
        //services.AddScoped<IAppRatingRepository, AppRatingDatabaseRepository>();


        services.AddDbContext<StakeholdersContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("stakeholders"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "stakeholders")));
    }
}
