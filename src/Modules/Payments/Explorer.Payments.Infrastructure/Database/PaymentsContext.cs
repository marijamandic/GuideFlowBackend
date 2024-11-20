using Explorer.Payments.Core.Domain.PurchaseTokens;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Payments.Infrastructure.Database;

public class PaymentsContext : DbContext
{
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<Item> ShoppingCartItems { get; set; }
    public DbSet<TourPurchaseToken> TourPurchaseTokens { get; set; }

    public PaymentsContext(DbContextOptions<PaymentsContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("payments");

        ConfigureShoppingCart(modelBuilder);
    }

    private static void ConfigureShoppingCart(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShoppingCart>()
            .HasMany(sc => sc.Items)
            .WithOne()
            .HasForeignKey(si => si.ShoppingCartId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
