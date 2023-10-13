using Domain;

namespace Application.Group;

public interface IBackupRepository
{
    List<Domain.Backup> GetAllBackups();

    public Backup GetLatestBackup(int groupId);

    Domain.Backup CreateBackup(Backup request);
}