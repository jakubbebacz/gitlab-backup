using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class GitLabDbContext : DbContext
{
    public GitLabDbContext(DbContextOptions<GitLabDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Label>()
            .HasOne<Backup>(l => l.Backup)
            .WithMany(b => b.Labels)
            .HasForeignKey(l => l.BackupId);
        
        modelBuilder.Entity<Milestone>()
            .HasOne<Backup>(m => m.Backup)
            .WithMany(b => b.Milestones)
            .HasForeignKey(m => m.BackupId);
    }
    
    public DbSet<Backup> Backups { get; set; }

    public DbSet<Label> Labels { get; set; }
    
    public DbSet<Milestone> Milestones { get; set; }
}