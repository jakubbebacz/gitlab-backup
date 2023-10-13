using Application.Group;
using Domain;

namespace Infrastructure.Repository;

public class BackupRepository : IBackupRepository
{
    private readonly GitLabDbContext _gitLabDbContext;

    public BackupRepository(GitLabDbContext gitLabDbContext)
    {
        _gitLabDbContext = gitLabDbContext;
    }
    
    public List<Domain.Backup> GetAllBackups()
    {
        var data = _gitLabDbContext.Backups.ToList();
        var latestBackups = data.OfType<Backup>()
            .GroupBy(b => b.GroupId)
            .Select(b => b.MaxBy(x => x.CreatedAt))
            .ToList();

        return latestBackups;
    }

    public Backup GetLatestBackup(int groupId)
    {
        return _gitLabDbContext.Backups.LastOrDefault(b => b.GroupId == groupId);
    }

    public Backup CreateBackup(Backup request)
    {
        _gitLabDbContext.Backups.Add(request);
        _gitLabDbContext.SaveChanges();

        return request;
    }
}