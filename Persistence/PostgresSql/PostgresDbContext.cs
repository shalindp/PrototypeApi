using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PrototypeBackend.Entities;

namespace Persistence.PostgresSql;

public class PostgresDbContext : DbContext
{
    public virtual DbSet<UserEntity> Users { get; set; }
    public virtual DbSet<ProfileEntity> Profiles { get; set; }
    public virtual DbSet<GenderIdentityEntity> Genders { get; set; }
    public virtual DbSet<OccupationEntity> Occupations { get; set; }
    public virtual DbSet<InterestEntity> Interests { get; set; }
    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public PostgresDbContext()
    {
    }

    public PostgresDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserEntity>(c =>
        {
            c.HasOne(user => user.Profile).WithOne(profile => profile.User)
                .HasForeignKey<ProfileEntity>(u => u.ProfileId);
        });

        modelBuilder.Entity<ProfileEntity>(c =>
        {
            // c.HasOne(p => p.GenderIdentity)
            //     .WithMany()
            //     .HasForeignKey(p => p.GenderIdentityId)
            //     .IsRequired();
            //
            // c.HasOne(p => p.PreferredGenderIdentity)
            //     .WithMany()
            //     .HasForeignKey(p => p.PreferredGenderIdentityId)
            //     .IsRequired();

            // c.HasOne(p => p.Occupation)
            //     .WithMany()
            //     .HasForeignKey(p => p.OccupationId)
            //     .IsRequired();

            // c.HasOne(p => p.InterestCollection)
            //     .WithMany()
            //     .HasForeignKey(p => p.InterestCollectionId);
        });
        //
        // modelBuilder.Entity<InterestCollection>(c =>
        // {
        //     c.HasOne(u => u.Interests).WithMany().HasForeignKey(u => u.InterestCollectionId);
        // });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.LogTo(action: Console.WriteLine, minimumLevel: LogLevel.Information);
        options.UseNpgsql(
            "User ID=test;Password=test;Server=localhost;Port=5432;Database=dev; Integrated Security=true;Pooling=true;IncludeErrorDetail=true;");
    }
}