using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.Club;
using Explorer.Stakeholders.Core.Domain.Problems;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database;

public class StakeholdersContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<Problem> Problems { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Club> Clubs { get; set; }
    public DbSet<ClubInvitation> ClubInvitations { get; set; }
    public DbSet<ClubMember> ClubMembers { get; set; }
    public DbSet<ClubRequest> ClubRequests { get; set; }
    public DbSet<ProfileInfo> Profiles { get; set; }
    public DbSet<ClubPost> ClubPosts { get; set; }

    public DbSet<AppRating> Ratings { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<ProblemNotification> ProblemNotifications { get; set; }
    public DbSet<Tourist> Tourists { get; set; }

    public StakeholdersContext(DbContextOptions<StakeholdersContext> options) : base(options) { }

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
        modelBuilder.Entity<User>().Property(tour => tour.Location).HasColumnType("jsonb");


        ConfigureStakeholder(modelBuilder);
        ConfigureClubInvitation(modelBuilder);
        ConfigureProblem(modelBuilder);
        ConfigureNotifications(modelBuilder);
    }

    private static void ConfigureStakeholder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<Person>(s => s.UserId);

        modelBuilder.Entity<ProfileInfo>()
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<ProfileInfo>(s => s.UserId);

        modelBuilder.Entity<Tourist>()
            .ToTable("Tourists")
            .HasBaseType<User>();
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
    
    private static void ConfigureProblem(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Problem>()
            .Property(p => p.Details)
            .HasColumnType("jsonb");

        modelBuilder.Entity<Problem>()
            .Property(p => p.Resolution)
            .HasColumnType("jsonb");

        modelBuilder.Entity<Problem>()
            .HasMany(p => p.Messages)
            .WithOne()
            .HasForeignKey(m => m.ProblemId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void ConfigureNotifications(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Notification>()
            .ToTable("Notifications")
            .HasKey(n => n.Id);

        modelBuilder.Entity<Notification>()
            .Property(n => n.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<ProblemNotification>()
            .ToTable("ProblemNotifications")
            .HasBaseType<Notification>();
    }
}