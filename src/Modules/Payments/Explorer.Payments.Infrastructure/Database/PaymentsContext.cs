using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.Payments;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Payments.Infrastructure.Database;

public class PaymentsContext : DbContext
{
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<Item> ShoppingCartItems { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<PaymentItem> PaymentItems { get; set; }
    public DbSet<TourPurchaseToken> TourPurchaseTokens { get; set; }
    public DbSet<TourBundle> TourBundles { get; set; }
    public DbSet<Sales> Sales { get; set; }

    public PaymentsContext(DbContextOptions<PaymentsContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("payments");

        ConfigureShoppingCart(modelBuilder);
        ConfigurePayment(modelBuilder);
        ConfigureSales(modelBuilder);
    }

    private static void ConfigureShoppingCart(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShoppingCart>()
            .HasMany(sc => sc.Items)
            .WithOne()
            .HasForeignKey(si => si.ShoppingCartId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private void ConfigurePayment(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Payment>()
                    .HasMany(p => p.PaymentItems)
                    .WithOne()
                    .HasForeignKey(pi => pi.PaymentId)
                    .OnDelete(DeleteBehavior.Cascade);
    }
    private static void ConfigureSales(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sales>()
            .Property(s => s.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Sales>()
            .ToTable(s => s.HasCheckConstraint("CK_Sales_EndsAt_Within_Range", "\"EndsAt\" >= \"CreatedAt\" AND \"EndsAt\" <= \"CreatedAt\" + INTERVAL '14 days'"));
    }
}
