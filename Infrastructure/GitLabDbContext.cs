using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class GitLabDbContext : DbContext
{
    public GitLabDbContext(DbContextOptions<GitLabDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Domain.Backup> Backups { get; set; }

    public DbSet<Domain.Label> Labels { get; set; }
    
    public DbSet<Domain.Milestone> Milestones { get; set; }
}