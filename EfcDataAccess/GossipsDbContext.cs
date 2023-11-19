using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace EfcDataAccess;

public class GossipsDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../EfcDataAccess/Gossips.db");
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking); 
        optionsBuilder.EnableSensitiveDataLogging(); // Enable sensitive data logging
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>()
            .HasKey(post => post.PostId);

        modelBuilder.Entity<User>()
            .HasKey(user => user.UserId);
        
    }
}