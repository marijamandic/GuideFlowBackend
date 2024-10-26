using Explorer.Tours.Core.Domain;
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
    public DbSet<TourSpecifications> TourSpecifications { get; set; }


    public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");
    }
}

