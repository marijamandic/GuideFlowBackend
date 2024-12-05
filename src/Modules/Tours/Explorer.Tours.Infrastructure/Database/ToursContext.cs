using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Core.Domain.TourExecutions;
using Explorer.Tours.Core.Domain.Shopping;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace Explorer.Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<TourEquipment> TourEquipment { get; set; }
    public DbSet<Checkpoint> Checkpoint { get; set; }
    public DbSet<TourObject> TourObjects { get; set; }
    public DbSet<Tour> Tours { get; set; }
    public DbSet<EquipmentManagement> EquipmentManagements { get; set; }
    public DbSet<TourReview> TourReviews { get; set; }
    public DbSet<TourSpecification> TourSpecifications { get; set; }
    public DbSet<PublicPoint> PublicPoints { get; set; }
    public DbSet<TourExecution> TourExecutions { get; set; }
    public DbSet<CheckpointStatus> CheckpointStatuses { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<PurchaseToken> PurchaseTokens { get; set; }
    public DbSet<PublicPointNotification> PublicPointNotifications { get; set; }
    public DbSet<TransportRating> TransportRatings { get; set; }

    public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");
        ConfigureTour(modelBuilder);
    }

    private static void ConfigureTour(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tour>().Property(tour => tour.TransportDurations).HasColumnType("jsonb");
        modelBuilder.Entity<Tour>().Property(tour => tour.Price).HasColumnType("jsonb");
        modelBuilder.Entity<TourExecution>().HasMany(te => te.CheckpointsStatus).WithOne().OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<CheckpointStatus>()
            .HasOne(cs => cs.Checkpoint)
            .WithMany()
            .HasForeignKey(cs => cs.CheckpointId);
        modelBuilder.Entity<Tour>().HasMany(tr => tr.Reviews).WithOne().HasForeignKey(r=>r.TourId);
        modelBuilder.Entity<TourSpecification>()
            .HasMany(ts => ts.TransportRatings)
            .WithOne(tr => tr.TourSpecification)
            .HasForeignKey(tr => tr.TourSpecificationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

