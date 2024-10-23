using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.Club;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database;

public class StakeholdersContext : DbContext
{

    public DbSet<User> Users { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<Problem> Problem { get; set; }
    public DbSet<Club> Clubs { get; set; }
    public DbSet<ClubInvitation> ClubInvitations { get; set; }
    public DbSet<ClubMember> ClubMembers { get; set; }
    public DbSet<ClubRequest> ClubRequests { get; set; }

    public StakeholdersContext(DbContextOptions<StakeholdersContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("stakeholders");

        modelBuilder.Entity<ClubRequest>()
       .Property(e => e.Status)
       .HasConversion(
           v => v.ToString(),  
           v => Enum.Parse<ClubRequestStatus>(v)
       );

        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

        
        ConfigureStakeholder(modelBuilder);
        ConfigureClubInvitation(modelBuilder);
        

    }

    private static void ConfigureStakeholder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<Person>(s => s.UserId);
    }

    private static void ConfigureClubInvitation(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClubInvitation>()
            .HasKey(ci => ci.Id);

        modelBuilder.Entity<ClubInvitation>()
            .Property(ci => ci.Status)
            .HasConversion<string>()
            .IsRequired();

        modelBuilder.Entity<ClubInvitation>()
            .Property(ci => ci.TouristID)
            .IsRequired();

        modelBuilder.Entity<ClubInvitation>()
            .Property(ci => ci.ClubId)
            .IsRequired();
    }
    
}