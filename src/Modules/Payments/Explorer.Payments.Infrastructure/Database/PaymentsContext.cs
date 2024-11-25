using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.Payments;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using Explorer.Stakeholders.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Payments.Infrastructure.Database;

public class PaymentsContext : DbContext
{
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<Item> ShoppingCartItems { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<PaymentItem> PaymentItems { get; set; }
    public DbSet<TourPurchaseToken> TourPurchaseTokens { get; set; }

    public DbSet<Coupon> Coupons { get; set; }

    public PaymentsContext(DbContextOptions<PaymentsContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("payments");

        ConfigureShoppingCart(modelBuilder);
        ConfigurePayment(modelBuilder);
        ConfigureCoupon(modelBuilder);
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

    private static void ConfigureCoupon(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.Property(c => c.AuthorId).IsRequired();
            entity.Property(c => c.TourId).IsRequired(false);

            entity.Property(c => c.Code)
                .IsRequired()
                .HasMaxLength(8);

            entity.Property(c => c.Discount)
                .IsRequired();

            entity.Property(c => c.ExpiryDate)
                .IsRequired(false);

            entity.Property(c => c.ValidForAllTours)
                .IsRequired();

            entity.Property(c => c.Redeemed)
                .HasDefaultValue(false);
        });
    }
}
