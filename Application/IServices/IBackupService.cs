using Application.Models.Backup;

namespace Application.IServices;

public interface IBackupService
{
    public Task<List<BackupResponse>> GetAllBackups();

    Task<Guid> CreateBackup(int groupId, bool isSimple);

    Task<Guid> RestoreBackup(int groupId);
}