using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.API.Internal;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Mappers;
using Explorer.Payments.Core.UseCases;
using Explorer.Payments.Infrastructure.Database;
using Explorer.Payments.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Explorer.Payments.Infrastructure;

public static class PaymentsStartup
{
    public static IServiceCollection ConfigurePaymentsModule(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(PaymentsProfile).Assembly);
        SetupCore(services);
        SetupInfrastructure(services);
        return services;
    }

    private static void SetupCore(IServiceCollection services)
    {
        services.AddScoped<IShoppingCartService, ShoppingCartService>();
        services.AddScoped<ITourPurchaseTokenService, TourPurchaseTokenService>();
        services.AddScoped<IInternalPurchaseTokenService, TourPurchaseTokenService>();
        services.AddScoped<IPaymentService,PaymentService>();
        services.AddScoped<ICouponService, CouponService>();
        services.AddScoped<ITourBundleService, TourBundleService>();
        services.AddScoped<IInternalTourBundleService, TourBundleService>();
        services.AddScoped<ISalesService, SalesService>();
    }

    private static void SetupInfrastructure(IServiceCollection services)
    {
        services.AddScoped<IShoppingCartRepository, ShoppingCartDatabaseRepository>();
        services.AddScoped<ITourPurchaseTokenRepository,TourPurchaseTokenDatabaseRepository>();
        services.AddScoped<IPaymentRepository,PaymentDatabaseRepository>();
        services.AddScoped<ICouponRepository, CouponDatabaseRepository>();
        services.AddScoped<ITourBundleRepository,TourBundleRepository>();
        services.AddScoped<ISalesRepository, SalesDatabaseRepository>();

        services.AddDbContext<PaymentsContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("payments"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "payments")));
    }
}
