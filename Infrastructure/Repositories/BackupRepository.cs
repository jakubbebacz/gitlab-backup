using Application.IRepositories;
using Application.Models.Backup;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BackupRepository : IBackupRepository
{
    private readonly GitLabDbContext _gitLabDbContext;

    public BackupRepository(GitLabDbContext gitLabDbContext)
    {
        _gitLabDbContext = gitLabDbContext;
    }
    
    public async Task<List<Backup>> GetAllBackups()
    {
        var data = await _gitLabDbContext.Backups.ToListAsync();
        var latestBackups = data.OfType<Backup>()
            .GroupBy(b => b.GroupId)
            .Select(b => b.MaxBy(x => x.CreatedAt))
            .ToList();

        return latestBackups;
    }

    public async Task<Backup> GetLatestBackup(int groupId)
    {
        var backup = await _gitLabDbContext.Backups
            .LastOrDefaultAsync(b => b.GroupId == groupId);
        return backup;
    }

    public async Task<Guid> CreateBackup(CreateBackupRequest request)
    {
        var backup = new Backup
        {
            BackupId = new Guid(),
            GroupId = request.GroupId,
            BackupName = request.BackupName,
            BackupPath = request.BackupPath,
            BackupDescription = request.BackupDescription,
            Visibility = request.Visibility,
            CreatedAt = request.CreatedAt
        };
        
        var response = _gitLabDbContext.Backups.Add(backup);
        await _gitLabDbContext.SaveChangesAsync();

        return response.Entity.BackupId;
    }
}