using Application.Models.Backup;
using Domain;

namespace Application.IRepositories;

public interface IBackupRepository
{
    Task<List<Backup>> GetAllBackups();

    public Task<Backup> GetLatestBackup(int groupId);

    Task<Guid> CreateBackup(CreateBackupRequest backup);
}