using Explorer.Stakeholders.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database;

public class StakeholdersContext : DbContext
{

    public DbSet<User> Users { get; set; }
    public DbSet<Person> People { get; set; }

    public DbSet<ClubRequest> ClubRequests { get; set; }
    public StakeholdersContext(DbContextOptions<StakeholdersContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("stakeholders");

        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

        ConfigureStakeholder(modelBuilder);
        ConfigureClubRequest(modelBuilder);

    }

    private static void ConfigureStakeholder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<Person>(s => s.UserId);
    }

    private static void ConfigureClubRequest(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClubRequest>()
            .HasKey(cr => cr.Id);

        modelBuilder.Entity<ClubRequest>()
            .Property(cr => cr.Status)
            .HasConversion<string>()
            .IsRequired();

        modelBuilder.Entity<ClubRequest>()
            .Property(cr => cr.TouristId)
            .IsRequired();

        modelBuilder.Entity<ClubRequest>()
            .Property(cr => cr.ClubId)
            .IsRequired();
    }
}